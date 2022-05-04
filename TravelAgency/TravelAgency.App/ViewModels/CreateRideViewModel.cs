using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Services;
using TravelAgency.BL.Facades;
using TravelAgency.BL.Models;
using TravelAgency.App.Messages;
using TravelAgency.App.Commands;
using TravelAgency.App.Extensions;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.App.Wrappers;

namespace TravelAgency.App.ViewModels
{
    public class CreateRideViewModel : ViewModelBase, ICreateRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;
        private readonly CarFacade _carFacade;
        private readonly ShareRideFacade _shareRideFacade ;
        private readonly IMessageDialogService _messageDialogService;

        private Guid _idUser;
        private ShareRideWrapper? _model = new ShareRideDetailModel(string.Empty, string.Empty, default, default, ArriveTime: default, CarId: default, DriverId:Guid.Empty);

        public CreateRideViewModel(CarFacade carFacade, ShareRideFacade shareRideFacade, IMediator mediator, IMessageDialogService messageDialogService)
        {
            _mediator = mediator;
            _carFacade = carFacade;
            _shareRideFacade = shareRideFacade;
            _messageDialogService = messageDialogService;

            CarSelectedCommand = new RelayCommand<CarListModel>(CarSelected);
            SubmitCreation = new AsyncRelayCommand(SubmitCreationFunc, CanSave);
            GoBack = new RelayCommand(GoBackFunc);

            mediator.Register<CreateRideWindowMessage>(CreateRideWindowOpen);
        }

        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        public ICommand SubmitCreation { get; }
        public ICommand GoBack { get; }
        public ICommand CarSelectedCommand { get; }

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
            _idUser = userId;
        }

        private async Task CreateModel()
        {
            Model = await _shareRideFacade.GetAsync(Guid.Empty) ?? new(string.Empty, string.Empty, default, default, ArriveTime: default, CarId: Guid.Empty, DriverId: Guid.Empty);
        }

        private void CreateRideWindowOpen(CreateRideWindowMessage obj)
        {
            FillCarsObservableCollection(obj.userID);
            IsVisible = true;
            CreateModel();
        }

        private bool CanSave() => Model?.IsValid ?? false;
        private void GoBackFunc()
        {
            IsVisible = false;
        }
    }
}