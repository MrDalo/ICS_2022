using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.BL.Models;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using TravelAgency.App.Wrappers;
using TravelAgency.Common.Enums;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace TravelAgency.App.ViewModels
{
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;


        public UserDetailViewModel(
            UserFacade userFacade,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;
        }

        public UserWrapper? Model { get; private set; }

        public async Task LoadAsync(Guid id)
        {

        }

        public async Task SaveAsync()
        {

        }

        public async Task DeleteAsync()
        {

        }





    }
}
