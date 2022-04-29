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
    public class FilteredRidesViewModel : ViewModelBase, IFilteredRidesViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public ICommand GoBack { get; }


        public FilteredRidesViewModel(IMediator mediator)
        {
            _mediator = mediator;

            mediator.Register<FilteredRideWindowMessage>(FilteredRidesWindowOpen);

            GoBack = new RelayCommand(GoBackFunc);

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


        private void GoBackFunc()
        {
            IsVisible = false;

        }

        private void FilteredRidesWindowOpen(FilteredRideWindowMessage obj)
        {
            IsVisible = true;

        }



    }
}