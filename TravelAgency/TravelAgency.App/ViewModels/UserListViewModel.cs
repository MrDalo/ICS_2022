using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Extensions;
using TravelAgency.BL.Models;
using TravelAgency.App.Wrappers;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using TravelAgency.Common.Enums;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;
using TravelAgency.App.Services.MessageDialog;

namespace TravelAgency.App.ViewModels
{
    public class UserListViewModel : ViewModelBase, IUserListViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private UserListModel? _selectedUserListModel;
        private readonly IMessageDialogService _messageDialogService;

        public ICommand LogIn { get; }
        //public ICommand OpenRegistration { get; }

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
        }

        public ObservableCollection<UserListModel> Users { get; } = new();
        
        
        public ICommand UserNewCommand { get; }

        public ICommand UserSelectedCommand { get; }


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
                _mediator.Send(new LogInMessage());
                _mediator.Send(new SelectedMessage<UserWrapper> { Id = _selectedUserListModel.Id });
            }
            else
            {
                var choice = _messageDialogService.Show(
                    "Žiadny zvolený užívateľ :(",
                    $"Pred prihlásením zvoľte užívateľa.",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);
            }
        }

    }
}
