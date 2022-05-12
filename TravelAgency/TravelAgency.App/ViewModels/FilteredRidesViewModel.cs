using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Extensions;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.BL.Facades;
using TravelAgency.BL.Models;
using RelayCommand = TravelAgency.App.Commands.RelayCommand;

namespace TravelAgency.App.ViewModels
{
    public class FilteredRidesViewModel : ViewModelBase, IFilteredRidesViewModel
    {
        private readonly ShareRideFacade _shareRideFacade;
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;
        private bool _isVisible = false;
        private readonly IMessageDialogService _messageDialogService;

        private bool _arrivalSelected = false;

        public FilteredRidesViewModel(ShareRideFacade shareRideFacade, UserFacade userFacade, IMediator mediator, IMessageDialogService messageDialogService)
        {
            _shareRideFacade = shareRideFacade;
            _userFacade = userFacade;
            _mediator = mediator;
            _messageDialogService = messageDialogService;
            mediator.Register<FilteredRideWindowMessage>(FilteredRidesWindowOpen);

            GoBack = new RelayCommand(GoBackFunc);
            FilterRides = new AsyncRelayCommand(FilterShareRides);
            IncrementTime1 = new RelayCommand(IncrementTimeValue1);
            DecrementTime1 = new RelayCommand(DecrementTimeValue1);
            IncrementTime2 = new RelayCommand(IncrementTimeValue2);
            DecrementTime2 = new RelayCommand(DecrementTimeValue2);
            AddUserToShareRide = new AsyncRelayCommand(AddUserToShareRideFunc);
        }
        public ObservableCollection<ShareRideDetailModel> FilteredShareRides { get; set; } = new();

        public ICommand AddUserToShareRide { get; }
        public ICommand GoBack { get; }
        public ICommand FilterRides { get; }
        public ICommand IncrementTime1 { get; }
        public ICommand DecrementTime1 { get; }
        public ICommand IncrementTime2 { get; }
        public ICommand DecrementTime2 { get; }
        public DateTime TimeValue1 { get; set; }
        public DateTime TimeValue2 { get; set; }

        public string? FromPlace { get; set; }
        public string? ToPlace { get; set; }

        public ShareRideDetailModel? SelectedShareRide { get; set; }
        private Guid UserId { get; set; }

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

        private void GoBackFunc()
        {
            IsVisible = false;

        }

        private async Task FilterShareRides()
        {
            IEnumerable<ShareRideDetailModel> newFilteredShareRides;
            if (ArrivalSelected)
            {
                // Prichod
                newFilteredShareRides = await _shareRideFacade.GetFilteredShareRidesAsync(startLocation: FromPlace, destinationLocation: ToPlace, finishTimeFrom: TimeValue1, finishTimeTo: TimeValue1 == TimeValue2 ? null : TimeValue2);

            }
            else
            {
                // Odchod
                newFilteredShareRides = await _shareRideFacade.GetFilteredShareRidesAsync(startLocation: FromPlace, destinationLocation: ToPlace, startTimeFrom: TimeValue1, startTimeTo: TimeValue1 == TimeValue2 ? null : TimeValue2);

            }
            FilteredShareRides.Clear();
            FilteredShareRides.AddRange(newFilteredShareRides);
        }

        private async Task AddUserToShareRideFunc()
        {
            var isShareRideFull = await _shareRideFacade.IsShareRideFull(SelectedShareRide);
            switch (isShareRideFull)
            {
                case 0:
                    {
                        var userModel = await _userFacade.GetAsync(UserId);
                        var isAdded = await _userFacade.SignUpForShareRideAsPassenger(userModel, SelectedShareRide);
                        if (isAdded)
                        {
                            var _ = _messageDialogService.Show(
                                $"Úspešne pridané",
                                $"Boli ste úspešne pridaný do jazdy",
                                MessageDialogButtonConfiguration.OK,
                                MessageDialogResult.OK);

                            _mediator.Send(new CloseSearchRideMessage());
                            IsVisible = false;
                        }
                        else
                        {
                            var _ = _messageDialogService.Show(
                                $"Časová kolízia",
                                $"Pridanie do jazdy zlyhalo. V tomto čase už existuje jazda",
                                MessageDialogButtonConfiguration.OK,
                                MessageDialogResult.OK);
                        }

                        break;
                    }
                case -1:
                    {
                        var _ = _messageDialogService.Show(
                            $"Pridanie zlyhalo",
                            $"Pridanie do jazdy zlyhalo",
                            MessageDialogButtonConfiguration.OK,
                            MessageDialogResult.OK);
                        break;
                    }
                default:
                    {
                        var _ = _messageDialogService.Show(
                            $"Pridanie zlyhalo",
                            $"Zvolená jazda už nemá voľné miesta",
                            MessageDialogButtonConfiguration.OK,
                            MessageDialogResult.OK);
                        break;
                    }
            }
        }

        private void FilteredRidesWindowOpen(FilteredRideWindowMessage obj)
        {
            FilteredShareRides.Clear();
            FilteredShareRides.AddRange(obj.filteredShareRide);
            FromPlace = obj.FromPlace;
            UserId = obj.UserId;
            ToPlace = obj.ToPlace;
            TimeValue1 = new DateTime(year: obj.Time1.Year, month: obj.Time1.Month, day: obj.Time1.Day, hour: obj.Time1.Hour, minute: obj.Time1.Minute, second: obj.Time1.Second);
            TimeValue2 = new DateTime(year: obj.Time1.Year, month: obj.Time1.Month, day: obj.Time1.Day, hour: obj.Time1.Hour, minute: obj.Time1.Minute, second: obj.Time1.Second);
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