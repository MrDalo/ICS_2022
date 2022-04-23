using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.BL.Models;
using TravelAgency.App.Wrappers;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using TravelAgency.Common.Enums;
using System.Collections.ObjectModel;
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



        }


        public async Task LoadAsync()
        {

        }


    }
}
