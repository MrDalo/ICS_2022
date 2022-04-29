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

        private bool _isVisible = false;
        private bool _isVisibleProfile = false;
        private UserWrapper? _model;

        public UserDetailViewModel(
            UserFacade userFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            
            // Registration Window
            mediator.Register<RegisterMessage>(OnOpenRegistration);

            // Profile Window
            mediator.Register<OpenUserCarsMessage>(OnUserCarsOpen);
            mediator.Register<OpenUserShareRidesMessage>(OnUserRidesOpen);
            mediator.Register<OpenProfileInfoMessage>(OnOpenProfile);
            mediator.Register<LogOutMessage>(OnLogOut);
            mediator.Register<HomeMessage>(OnHome);
        }

        private void OnOpenRegistration(RegisterMessage obj)
        {
            IsVisible = true;
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

        public UserWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }

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
    }
}
