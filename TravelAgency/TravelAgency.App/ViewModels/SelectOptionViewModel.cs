﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using TravelAgency.App.Services;
using TravelAgency.App.Messages;
using TravelAgency.App.Wrappers;

namespace TravelAgency.App.ViewModels
{
    public class SelectOptionViewModel : ViewModelBase, ISelectOptionViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public SelectOptionViewModel(IMediator mediator)
        {
            _mediator = mediator;

            CreateRide = new RelayCommand(CreateRideButton);
            SearchRide = new RelayCommand(SearchRideButton);

            mediator.Register<LogInMessage>(OnUserLogin);
            mediator.Register<LogOutMessage>(OnLogOut);
        }
        
        public ICommand CreateRide { get; set; }
        public ICommand SearchRide { get; set; }
        public Guid UserId { get; set; }

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        /*** Message processing ***/

        private void OnUserLogin(LogInMessage obj)
        {
            UserId = obj.ID;
            IsVisible = true;
        }

        private void OnLogOut(LogOutMessage obj)
        {
            IsVisible = false;
        }

        private void CreateRideButton()
        {
            _mediator.Send(new CreateRideWindowMessage(UserId));
        }
        private void SearchRideButton()
        {
            _mediator.Send(new OpenSearchRideMessage(UserId));

        }
    }
}
