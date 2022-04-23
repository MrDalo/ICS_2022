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
using TravelAgency.Common.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TravelAgency.App.ViewModels
{
    public class PassengerOfShareRideDetailViewModel : ViewModelBase
    {


        private readonly PassengerOfShareRideFacade _passengerOfShareRideFacade;
        private readonly IMediator _mediator;


        public PassengerOfShareRideDetailViewModel(
            PassengerOfShareRideFacade passengerOfShareRideFacade,
            IMediator mediator)
        {
            _passengerOfShareRideFacade = passengerOfShareRideFacade;
            _mediator = mediator;
        }

    }
}
