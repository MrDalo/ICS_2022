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

namespace TravelAgency.App.ViewModels
{
    public class UserListViewModel : ViewModelBase, IUserListViewModel
    {

        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;


        public UserListViewModel(
            UserFacade userFacade,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            UserSelectedCommand = new RelayCommand<UserListModel>(UserSelected);

            mediator.Register<UpdateMessage<UserWrapper>>(UserUpdated);

        }

        public ObservableCollection<UserListModel> Users { get; } = new();
        
        


        //public ICommand UserNewCommand { get; }

        public ICommand UserSelectedCommand { get; }


        private async void UserUpdated(UpdateMessage<UserWrapper> _) => await LoadAsync();


        private void UserSelected(UserListModel? userListModel)
        {
            if (userListModel is not null)
            {
                _mediator.Send(new SelectedMessage<UserWrapper> { Id = userListModel.Id });
            }
        }

        
        public async Task LoadAsync()
        {
            Users.Clear();
            
            var cars = await _userFacade.GetAll();
            Users.AddRange(cars);
        }

        public override void LoadInDesignMode()
        {
            Users.Add(new UserListModel(
                Login: "Patrik Of Sehnoutek"));

        }


    }
}
