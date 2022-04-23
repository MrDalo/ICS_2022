﻿using System;
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
    public class ShareRideListViewModel : ViewModelBase, IShareRideListViewModel
    {

        private readonly ShareRideFacade _shareRideFacade;
        private readonly IMediator _mediator;


        public ShareRideListViewModel(
            ShareRideFacade shareRideFacade,
            IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;
        }

        public async Task LoadAsync()
        {

        }


    }
}
