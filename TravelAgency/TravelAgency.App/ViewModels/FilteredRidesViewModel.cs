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

        private bool _arrivalSelected = false;
        public ObservableCollection<ShareRideDetailModel> filteredShareRides { get; set; }= new();

        private DateTime _TimeValue = DateTime.Now;



        public ICommand AddUserToShareRide { get; }

        public ICommand GoBack { get; }
        public ICommand FilterRides { get; }

        public ICommand IncrementTime1 { get; }
        public ICommand DecrementTime1 { get; }

        public ICommand IncrementTime2 { get; }
        public ICommand DecrementTime2 { get; }


        private readonly ShareRideFacade _shareRideFacade;
        private readonly UserFacade _userFacade;



        public FilteredRidesViewModel(ShareRideFacade shareRideFacade, UserFacade userFacade, IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _userFacade = userFacade;
            _mediator = mediator;

            mediator.Register<FilteredRideWindowMessage>(FilteredRidesWindowOpen);

            GoBack = new RelayCommand(GoBackFunc);
            FilterRides = new AsyncRelayCommand(FilterShareRides);
            IncrementTime1 = new RelayCommand(IncrementTimeValue1);
            DecrementTime1 = new RelayCommand(DecrementTimeValue1);
            IncrementTime2 = new RelayCommand(IncrementTimeValue2);
            DecrementTime2 = new RelayCommand(DecrementTimeValue2);
            AddUserToShareRide = new AsyncRelayCommand(AddUserToShareRideFunc);

        }

        public DateTime TimeValue1 { get; set; }

        public DateTime TimeValue2 { get; set; }

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public bool ArrivalSelected
        {
            get => _arrivalSelected;

            set
            {
                _arrivalSelected = value;
                OnPropertyChanged();
            }
        }

        public string? FromPlace { get; set; }
        public string? ToPlace { get; set; }

        public ShareRideDetailModel selectedShareRide { get; set; }
        private Guid UserId{ get; set; }

        private void GoBackFunc()
        {
            IsVisible = false;

        }

        private async Task FilterShareRides()
        {
            IEnumerable<ShareRideDetailModel> newFilteredShareRides;
            if (ArrivalSelected)
            {
                //Prichod
                newFilteredShareRides = await _shareRideFacade.GetFilteredShareRidesAsync(startLocation: FromPlace, destinationLocation: ToPlace, finishTimeFrom: TimeValue1, finishTimeTo: TimeValue1 == TimeValue2 ? null : TimeValue2);

            }
            else
            {
                //Odchod
                newFilteredShareRides = await _shareRideFacade.GetFilteredShareRidesAsync(startLocation: FromPlace, destinationLocation: ToPlace, startTimeFrom: TimeValue1, startTimeTo: TimeValue1 == TimeValue2 ? null : TimeValue2);

            }
            filteredShareRides.Clear();
            filteredShareRides.AddRange(newFilteredShareRides);
        }



        private async Task AddUserToShareRideFunc()
        {
            var isShareRideFull = await _shareRideFacade.IsShareRideFull(selectedShareRide);
            if (isShareRideFull == 0)
            {
                var userModel = await _userFacade.GetAsync(UserId);
                var isAdded = await _userFacade.SignUpForShareRideAsPassenger(userModel, selectedShareRide);
            }
            else if (isShareRideFull == -1)
            {
                //Error
            }
            else
            {
                //ShareRideIsFull
            }

        }

        private void FilteredRidesWindowOpen(FilteredRideWindowMessage obj)
        {
            filteredShareRides.Clear();
            filteredShareRides.AddRange(obj.filteredShareRide);
            FromPlace = obj.FromPlace;
            UserId = obj.UserId;
            ToPlace = obj.ToPlace;
            TimeValue1 = new DateTime(year: obj.Time1.Year, month: obj.Time1.Month, day: obj.Time1.Day, hour: obj.Time1.Hour, minute: obj.Time1.Minute, second: obj.Time1.Second);
            TimeValue2 = new  DateTime(year: obj.Time1.Year, month: obj.Time1.Month, day: obj.Time1.Day, hour: obj.Time1.Hour, minute: obj.Time1.Minute, second: obj.Time1.Second );
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