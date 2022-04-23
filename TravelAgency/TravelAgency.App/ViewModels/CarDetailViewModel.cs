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
    public class CarDetailViewModel : ViewModelBase 
    {

        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;


        public CarDetailViewModel(
            CarFacade carFacade,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _mediator = mediator;
        }


    }
}
