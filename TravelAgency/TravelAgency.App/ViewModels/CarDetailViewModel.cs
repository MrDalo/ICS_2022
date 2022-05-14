using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.App.Wrappers;
using TravelAgency.BL.Facades;
using AsyncRelayCommand = TravelAgency.App.Commands.AsyncRelayCommand;


namespace TravelAgency.App.ViewModels
{
    public class CarDetailViewModel : ViewModelBase, ICarDetailViewModel
    {
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;
        private CarWrapper? _model;

        private Guid _idUser;

        public CarDetailViewModel(
            CarFacade carFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            GoBack = new RelayCommand(GoBackFunc);

            mediator.Register<LoadUserProfile>(LoadCars);
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand GoBack { get; }

        public CarWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(Guid id)
        {
            Model = await _carFacade.GetAsync(id) ?? new(string.Empty, string.Empty, default, default, 0, _idUser);
        }

        public async Task SaveAsync()
        {
            if (!await _carFacade.CanIAddNewCar(_idUser))
            {
                var _ = _messageDialogService.Show(
                    $"Uloženie zlyhalo",
                    "Nemôžeš pridať viac ako 3 autá.",
                    MessageDialogButtonConfiguration.Ok,
                    MessageDialogResult.Ok);
                Model = null;
                return;
            }

            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            if (!Uri.IsWellFormedUriString(Model.ImgUrl, UriKind.Absolute))
            {
                _model.ImgUrl = "https://cdn-icons-png.flaticon.com/512/744/744465.png";
            }
            OnPropertyChanged();

            Model = await _carFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarWrapper> { Model = Model });

            // Hide window
            Model = null;
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    $"Vymazať",
                    $"Skutočne chcete vymazať auto s ŠPZ {Model?.LicensePlate}?",
                    MessageDialogButtonConfiguration.ÁnoNie,
                    MessageDialogResult.Nie);

                if (delete == MessageDialogResult.Nie) return;

                try
                {
                    await _carFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Vymazanie auta s ŠPZ {Model?.LicensePlate} zlyhalo!",
                        "Vymazanie zlyhalo",
                        MessageDialogButtonConfiguration.Ok,
                        MessageDialogResult.Ok);
                }

                _mediator.Send(new DeleteMessage<CarWrapper>
                {
                    Model = Model
                });
            }
            // Hide window
            Model = null;
        }

        /*** Message processing ***/

        private void LoadCars(LoadUserProfile obj)
        {
            _idUser = obj.Id;
        }

        private void GoBackFunc()
        {
            _mediator.Send(new UpdateMessage<CarWrapper> { Model = Model });
            Model = null;
        }
    }
}
