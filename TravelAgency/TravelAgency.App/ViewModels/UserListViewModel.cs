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
    public class UserListViewModel : ViewModelBase, IUserListViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private UserListModel? _selectedUserListModel;
        private readonly IMessageDialogService _messageDialogService;

        public UserListViewModel(
            UserFacade userFacade,
            IMediator mediator,
            IMessageDialogService messageDialogService)
        {
            _userFacade = userFacade;
            _mediator = mediator;
            _messageDialogService = messageDialogService;

            UserSelectedCommand = new RelayCommand<UserListModel>(UserSelected);
            UserNewCommand = new RelayCommand(UserNew);

            LogIn = new RelayCommand(LogInUser);

            mediator.Register<UpdateMessage<UserWrapper>>(UserUpdated);
            mediator.Register<LogOutMessage>(LogOutUser);
        }

        public ObservableCollection<UserListModel> Users { get; } = new();

        public ICommand UserNewCommand { get; }
        public ICommand UserSelectedCommand { get; }
        public ICommand LogIn { get; }

        private async void UserUpdated(UpdateMessage<UserWrapper> _) => await LoadAsync();

        private void UserNew()
        {
            _mediator.Send(new RegisterMessage());
            _mediator.Send(new NewMessage<UserWrapper>());
        }

        private void UserSelected(UserListModel? userListModel)
        {
            if (userListModel is not null)
            {
                _selectedUserListModel = userListModel;
            }
        }

        public async Task LoadAsync()
        {
            Users.Clear();

            var users = await _userFacade.GetAll();
            Users.AddRange(users);
        }

        private void LogInUser()
        {
            if (_selectedUserListModel is not null)
            {
                _mediator.Send(new LogInMessage(_selectedUserListModel.Id));
                _mediator.Send(new SelectedMessage<UserWrapper> { Id = _selectedUserListModel.Id });
            }
            else
            {
                var choice = _messageDialogService.Show(
                    "Žiadny zvolený užívateľ :(",
                    $"Pred prihlásením zvoľte užívateľa.",
                    MessageDialogButtonConfiguration.Ok,
                    MessageDialogResult.Ok);
            }
        }

        /*** Message processing ***/

        private async void LogOutUser(LogOutMessage obj)
        {
            _selectedUserListModel = null;
            await LoadAsync();
        }
    }
}
