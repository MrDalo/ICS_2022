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


        public SearchRideViewModel(IMediator mediator)
        {
            _mediator = mediator;

            mediator.Register<OpenSearchRideMessage>(OnSearchRideOpen);
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

        private void OnSearchRideOpen(OpenSearchRideMessage obj)
        {
            IsVisible = true;
        }

    }
}