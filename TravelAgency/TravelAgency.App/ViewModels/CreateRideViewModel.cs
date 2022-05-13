using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Commands;
using TravelAgency.App.Extensions;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.App.Wrappers;
using TravelAgency.BL.Facades;
using TravelAgency.BL.Models;

namespace TravelAgency.App.ViewModels
{
    public class CreateRideViewModel : ViewModelBase, ICreateRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;
        private bool _openCalendar1 = false;
        private bool _openCalendar2 = false;
        private readonly CarFacade _carFacade;
        private readonly ShareRideFacade _shareRideFacade;
        private readonly IMessageDialogService _messageDialogService;

        private Guid _idUser;
        private ShareRideWrapper? _model = new ShareRideDetailModel(string.Empty, string.Empty, default, default, ArriveTime: default, CarId: default, DriverId: Guid.Empty);

        public CreateRideViewModel(CarFacade carFacade, ShareRideFacade shareRideFacade, IMediator mediator, IMessageDialogService messageDialogService)
        {
            _mediator = mediator;
            _carFacade = carFacade;
            _shareRideFacade = shareRideFacade;
            _messageDialogService = messageDialogService;

            CarSelectedCommand = new RelayCommand<CarListModel>(CarSelected);
            SubmitCreation = new AsyncRelayCommand(SubmitCreationFunc, CanSave);
            GoBack = new RelayCommand(GoBackFunc);

            ShowCalendar1 = new RelayCommand(() => OpenCalendar1 = !OpenCalendar1);
            ShowCalendar2 = new RelayCommand(() => OpenCalendar2 = !OpenCalendar2);

            mediator.Register<CreateRideWindowMessage>(CreateRideWindowOpen);
        }

        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        public ICommand SubmitCreation { get; }
        public ICommand GoBack { get; }
        public ICommand CarSelectedCommand { get; }
        public ICommand ShowCalendar1 { get; }
        public ICommand ShowCalendar2 { get; }

        private DateTime _currentDate1 = DateTime.Now;
        private DateTime _currentDate2 = DateTime.Now;
        public ShareRideWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
        public bool OpenCalendar1
        {
            get => _openCalendar1;

            set
            {
                _openCalendar1 = value;
                OnPropertyChanged();
            }
        }
        public DateTime CurrentDate1
        {
            get => _currentDate1;

            set
            {
                _currentDate1 = value;
                OnPropertyChanged();
            }
        }
        public bool OpenCalendar2
        {
            get => _openCalendar2;

            set
            {
                _openCalendar2 = value;
                OnPropertyChanged();
            }
        }
        public DateTime CurrentDate2
        {
            get => _currentDate2;

            set
            {
                _currentDate2 = value;
                OnPropertyChanged();
            }
        }
        private void CarSelected(CarListModel? carListModel)
        {
            if (carListModel is not null)
            {
                _model.CarId = carListModel.Id;
            }
        }

        public async Task SubmitCreationFunc()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            if (Model.DriverId == Guid.Empty)
            {
                _model.DriverId = _idUser;
            }

            OnPropertyChanged();

            Model = await _shareRideFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<ShareRideWrapper> { Model = Model });

            var _ = _messageDialogService.Show(
                $"Vytvorenie jazdy",
                $"Vytvorenie jazdy prebehlo v poriadku.",
                MessageDialogButtonConfiguration.OK,
                MessageDialogResult.OK);
            // Hide Window
            Model = null;
            IsVisible = false;
        }

        private async Task FillCarsObservableCollection(Guid userId)
        {
            Cars.Clear();
            Cars.AddRange(await _carFacade.GetAllUserCars(userId));
            if (Cars.Count == 0)
            {
                var _ = _messageDialogService.Show(
                    $"Nemožno vytvoriť",
                    $"Nemožno vytvoriť jazdu, nevlastníte žiadne auto",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);
                IsVisible = false;
                return;

            }
            _idUser = userId;
        }

        private async Task CreateModel()
        {
            Model = await _shareRideFacade.GetAsync(Guid.Empty) ?? new(string.Empty, string.Empty, default, default, ArriveTime: default, CarId: Guid.Empty, DriverId: Guid.Empty);
        }

        private async void CreateRideWindowOpen(CreateRideWindowMessage obj)
        {
            await FillCarsObservableCollection(obj.userID);
            IsVisible = Cars.Count != 0;
            await CreateModel();
        }

        private bool CanSave() => Model?.IsValid ?? false;
        private void GoBackFunc()
        {
            IsVisible = false;
        }
    }
}