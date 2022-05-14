using System;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.BL.Models;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using TravelAgency.App.Wrappers;
using System.Windows.Input;

namespace TravelAgency.App.ViewModels
{
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;

        private bool _isVisible = false;
        private bool _isVisibleProfile = false;
        private bool _editURL = false;
        private UserWrapper? _model;

        public UserDetailViewModel(
            UserFacade userFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            GoBack = new RelayCommand(GoBackFromRegistration);
            ShowTextBox = new RelayCommand(() => EditURL = !EditURL);

            // Registration Window
            mediator.Register<RegisterMessage>(OnOpenRegistration);

            // Profile Window
            mediator.Register<OpenUserCarsMessage>(OnUserCarsOpen);
            mediator.Register<OpenUserShareRidesMessage>(OnUserRidesOpen);
            mediator.Register<OpenProfileInfoMessage>(OnOpenProfile);
            mediator.Register<LogOutMessage>(OnLogOut);
            mediator.Register<HomeMessage>(OnHome);
        }

        public ICommand SaveCommand { get; }
        public ICommand GoBack { get; }
        public ICommand ShowTextBox { get; }

        public UserWrapper? Model
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

        public bool IsVisibleProfile
        {
            get => _isVisibleProfile;

            set
            {
                _isVisibleProfile = value;
                OnPropertyChanged();
            }
        }

        public bool EditURL
        {
            get => _editURL;

            set
            {
                _editURL = value;
                OnPropertyChanged();
            }
        }
        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UserDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            if (!Uri.IsWellFormedUriString(Model.PhotoUrl, UriKind.Absolute))
            {
                _model.PhotoUrl = "https://icons.veryicon.com/png/o/business/multi-color-financial-and-business-icons/user-139.png";
            }

            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper>{Model = Model});
           
            
            
                var _ = _messageDialogService.Show(
                    $"Uloženie",
                    $"Uloženie prebehlo v poriadku.",
                    MessageDialogButtonConfiguration.Ok,
                    MessageDialogResult.Ok);

            IsVisible = false;
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            _mediator.Send(new DeleteMessage<UserWrapper>
            {
                Model = Model
            });
        }

        /*** Message processing ***/

        private void OnOpenRegistration(RegisterMessage obj)
        {
            IsVisible = true;
        }

        private void GoBackFromRegistration()
        {
            IsVisible = false;
        }

        private void OnOpenProfile(OpenProfileInfoMessage obj)
        {
            IsVisibleProfile = true;
        }

        private void OnUserCarsOpen(OpenUserCarsMessage obj)
        {
            IsVisibleProfile = false;
        }

        private void OnUserRidesOpen(OpenUserShareRidesMessage obj)
        {
            IsVisibleProfile = false;
        }

        private void OnLogOut(LogOutMessage obj)
        {
            IsVisibleProfile = false;
        }
        private void OnHome(HomeMessage obj)
        {
            IsVisibleProfile = false;
        }
    }
}
