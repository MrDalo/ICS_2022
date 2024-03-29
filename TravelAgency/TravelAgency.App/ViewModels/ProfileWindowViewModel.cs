﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Services;
using TravelAgency.App.Messages;
using TravelAgency.App.Commands;

namespace TravelAgency.App.ViewModels
{
    public class ProfileWindowViewModel : ViewModelBase, IProfileWindowViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public ICommand LogOut { get; }
        public ICommand ToProfileInfo { get; }
        public ICommand ToUserCars { get; }
        public ICommand ToUserShareRides { get; }

        public ProfileWindowViewModel(IMediator mediator)
        {
            _mediator = mediator;

            mediator.Register<OpenProfileInfoMessage>(OnOpenProfile);
            LogOut = new RelayCommand(LogOutUser);
            ToProfileInfo = new RelayCommand(OpenProfileInfo);
            ToUserCars = new RelayCommand(OpenUserCars);
            ToUserShareRides = new RelayCommand(OpenUserShareRides);
        }

        public bool IsVisible
        {
            get { return _isVisible; }

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        private void OpenUserCars()
        {
            _mediator.Send(new OpenUserCarsMessage());
        }

        private void OpenProfileInfo()
        {
            _mediator.Send(new OpenProfileInfoMessage());
        }

        private void OpenUserShareRides()
        {
            _mediator.Send(new OpenUserShareRidesMessage());
        }

        private void LogOutUser()
        {
            IsVisible = false;
            _mediator.Send(new LogOutMessage());
        }

        private void OnOpenProfile(OpenProfileInfoMessage obj)
        {
            IsVisible = true;
        }

    }
}