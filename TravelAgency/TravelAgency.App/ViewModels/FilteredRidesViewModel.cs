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

        public ICommand GoBack { get; }
        public ICommand FilterRides { get; }
        private readonly ShareRideFacade _shareRideFacade;


        public FilteredRidesViewModel(ShareRideFacade shareRideFacade, IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;

            mediator.Register<FilteredRideWindowMessage>(FilteredRidesWindowOpen);

            GoBack = new RelayCommand(GoBackFunc);
            FilterRides = new AsyncRelayCommand(FilterShareRides);

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
            filteredShareRides.AddRange(obj.filteredShareRide);
            FromPlace = obj.FromPlace;
            ToPlace = obj.ToPlace;
            IsVisible = true;
            

        }



    }
}