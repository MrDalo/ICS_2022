using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.BL.Facades;
using RelayCommand = TravelAgency.App.Commands.RelayCommand;

namespace TravelAgency.App.ViewModels
{
    public class SearchRideViewModel : ViewModelBase, ISearchRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;
        private bool _openCalendar = false;

        public SearchRideViewModel(ShareRideFacade shareRideFacade, IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;

            mediator.Register<OpenSearchRideMessage>(OnSearchRideOpen);
            mediator.Register<CloseSearchRideMessage>(OnSearchRideClose);
            GoBack = new RelayCommand(GoBackFunc);
            FilteredRides = new AsyncRelayCommand(FilterRidesButton);
            IncrementTime = new RelayCommand(IncrementTimeValue);
            DecrementTime = new RelayCommand(DecrementTimeValue);
            ShowCalendar = new RelayCommand(() => OpenCalendar = !OpenCalendar);
        }

        public ICommand GoBack { get; }
        public ICommand FilteredRides { get; }
        public ICommand IncrementTime { get; }
        public ICommand DecrementTime { get; }
        public ICommand ShowCalendar { get; }

        private readonly ShareRideFacade _shareRideFacade;
        public string? FromPlace { get; set; }
        public string? ToPlace { get; set; }
        private Guid UserId { get; set; }

        private DateTime _timeValue = DateTime.Now;
        private DateTime _currentDate = DateTime.Now;

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
        public bool OpenCalendar
        {
            get => _openCalendar;

            set
            {
                _openCalendar = value;
                OnPropertyChanged();
            }
        }
        public DateTime TimeValue
        {
            get => _timeValue;

            set
            {
                _timeValue = value;
                OnPropertyChanged();
            }
        }
        public DateTime CurrentDate
        {
            get => _currentDate;

            set
            {
                _currentDate = value;
                OnPropertyChanged();
            }
        }
        private void GoBackFunc()
        {
            IsVisible = false;

        }
        private async Task FilterRidesButton()
        {
            var newTimeForFiltering = new DateTime(year: CurrentDate.Year, month: CurrentDate.Month,
                day: CurrentDate.Day, hour: TimeValue.Hour, minute: TimeValue.Minute, second: 0);
            var filteredShareRides = await _shareRideFacade.GetFilteredShareRidesAsync(startTimeFrom: newTimeForFiltering, finishTimeFrom: null,
                startLocation: FromPlace, destinationLocation: ToPlace);

            _mediator.Send(new FilteredRideWindowMessage(filteredShareRides, FromPlace, ToPlace, newTimeForFiltering, newTimeForFiltering, UserId));
        }

        private void OnSearchRideOpen(OpenSearchRideMessage obj)
        {
            UserId = obj.UserId;
            IsVisible = true;
        }

        private void OnSearchRideClose(CloseSearchRideMessage obj)
        {
            IsVisible = false;
        }

        private void IncrementTimeValue()
        {
            TimeValue = TimeValue.AddMinutes(30);
        }
        private void DecrementTimeValue()
        {
            TimeValue = TimeValue.AddMinutes(-30);
        }
    }
}