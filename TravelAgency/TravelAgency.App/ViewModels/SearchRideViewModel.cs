using System;
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
    public class SearchRideViewModel : ViewModelBase, ISearchRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        private DateTime _TimeValue = DateTime.Now;
        private DateTime _currentDate = DateTime.Now;

        public ICommand GoBack { get; }
        public ICommand FilteredRides { get; }

        public ICommand IncrementTime { get; }
        public ICommand DecrementTime { get; }


        public SearchRideViewModel(IMediator mediator)
        {
            _mediator = mediator;

            mediator.Register<OpenSearchRideMessage>(OnSearchRideOpen);
            GoBack = new RelayCommand(GoBackFunc);
            FilteredRides = new RelayCommand(FilterRidesButton);
            IncrementTime = new RelayCommand(IncrementTimeValue);
            DecrementTime = new RelayCommand(DecrementTimeValue);
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

        public DateTime TimeValue
        {
            get => _TimeValue;

            set
            {
                _TimeValue = value;
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

        private void FilterRidesButton()
        {
            _mediator.Send(new FilteredRideWindowMessage());

        }

        private void OnSearchRideOpen(OpenSearchRideMessage obj)
        {
            IsVisible = true;
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