using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using TravelAgency.App.Services;
using TravelAgency.App.Messages;
using TravelAgency.App.Extensions;
using TravelAgency.BL.Models;
using TravelAgency.BL.Facades;
using RelayCommand = TravelAgency.App.Commands.RelayCommand;

namespace TravelAgency.App.ViewModels
{
    public class FilteredRidesViewModel : ViewModelBase, IFilteredRidesViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;
        public ObservableCollection<ShareRideDetailModel> filteredShareRides { get; set; }= new();

        private DateTime _TimeValue1 = DateTime.Now;

        private DateTime _TimeValue2 = DateTime.Now;

        public ICommand GoBack { get; }
        public ICommand FilterRides { get; }

        public ICommand IncrementTime1 { get; }
        public ICommand DecrementTime1 { get; }

        public ICommand IncrementTime2 { get; }
        public ICommand DecrementTime2 { get; }


        private readonly ShareRideFacade _shareRideFacade;



        public FilteredRidesViewModel(ShareRideFacade shareRideFacade, IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;

            mediator.Register<FilteredRideWindowMessage>(FilteredRidesWindowOpen);

            GoBack = new RelayCommand(GoBackFunc);
            FilterRides = new AsyncRelayCommand(FilterShareRides);
            IncrementTime1 = new RelayCommand(IncrementTimeValue1);
            DecrementTime1 = new RelayCommand(DecrementTimeValue1);
            IncrementTime2 = new RelayCommand(IncrementTimeValue2);
            DecrementTime2 = new RelayCommand(DecrementTimeValue2);

        }

        public DateTime TimeValue1
        {
            get => _TimeValue1;

            set
            {
                _TimeValue1 = value;
                OnPropertyChanged();
            }
        }

        public DateTime TimeValue2
        {
            get => _TimeValue2;

            set
            {
                _TimeValue2 = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public string? FromPlace { get; set; }
        public string? ToPlace { get; set; }

        private void GoBackFunc()
        {
            IsVisible = false;

        }

        private async Task FilterShareRides()
        {
            
            var newFilteredShareRides = await _shareRideFacade.GetFilteredShareRidesAsync(startLocation: FromPlace, destinationLocation: ToPlace);
            filteredShareRides.Clear();
            filteredShareRides.AddRange(newFilteredShareRides);
        }



        private void FilteredRidesWindowOpen(FilteredRideWindowMessage obj)
        {
            filteredShareRides.Clear();
            filteredShareRides.AddRange(obj.filteredShareRide);
            FromPlace = obj.FromPlace;
            ToPlace = obj.ToPlace;
            IsVisible = true;
            

        }

        private void IncrementTimeValue1()
        {
            TimeValue1 = TimeValue1.AddMinutes(30);
            if (DateTime.Compare(TimeValue1, TimeValue2) > 0)
            {
                TimeValue1 = TimeValue1.AddMinutes(-30);
            }
        }
        private void DecrementTimeValue1()
        {
            TimeValue1 = TimeValue1.AddMinutes(-30);
        }

        private void IncrementTimeValue2()
        {
            TimeValue2 = TimeValue2.AddMinutes(30);
            
        }
        private void DecrementTimeValue2()
        {
            TimeValue2 = TimeValue2.AddMinutes(-30);
            if (DateTime.Compare(TimeValue1, TimeValue2) > 0)
            {
                TimeValue2 = TimeValue2.AddMinutes(30);
            }
        }



    }
}