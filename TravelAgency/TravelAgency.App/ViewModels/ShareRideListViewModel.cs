using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Commands;
using TravelAgency.App.Extensions;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Wrappers;
using TravelAgency.BL.Facades;
using TravelAgency.BL.Models;

namespace TravelAgency.App.ViewModels
{
    public class ShareRideListViewModel : ViewModelBase, IShareRideListViewModel
    {

        private readonly ShareRideFacade _shareRideFacade;
        private readonly IMediator _mediator;

        // User share rides in profile
        private bool _isVisible = false;


        public ShareRideListViewModel(
            ShareRideFacade shareRideFacade,
            IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;
            ShareRideSelectedCommand = new RelayCommand<ShareRideListModel>(ShareRideSelected);
            ShareRideNewCommand = new RelayCommand(ShareRideNew);

            mediator.Register<DeleteMessage<ShareRideWrapper>>(ShareRideDeleted);
            mediator.Register<UpdateMessage<ShareRideWrapper>>(ShareRideUpdated);

            mediator.Register<OpenUserCarsMessage>(OnUserCarsOpen);
            mediator.Register<OpenUserShareRidesMessage>(OnUserRidesOpen);
            mediator.Register<OpenProfileInfoMessage>(OnOpenProfile);
            mediator.Register<LogOutMessage>(OnLogOut);
            mediator.Register<HomeMessage>(OnHome);

            mediator.Register<LoadUserProfile>(LoadShareRides);
        }

        public ObservableCollection<ShareRideListModel> DriverShareRides { get; set; } = new();
        public ObservableCollection<ShareRideListModel> PassengerShareRides { get; set; } = new();

        private Guid _userGuid;
        public ICommand ShareRideSelectedCommand { get; }
        public ICommand ShareRideNewCommand { get; }

        private void ShareRideNew() => _mediator.Send(new NewMessage<ShareRideWrapper>());

        private void ShareRideSelected(ShareRideListModel? shareRides) => _mediator.Send(new SelectedMessage<ShareRideWrapper> { Id = shareRides?.Id });

        private async void ShareRideDeleted(DeleteMessage<ShareRideWrapper> _) => await LoadAsync();

        private async void ShareRideUpdated(UpdateMessage<ShareRideWrapper> _) => await LoadAsync();


        public async Task LoadAsync()
        {
            // user can edit only share rides as a driver
            DriverShareRides.Clear();
            var shareRides = await _shareRideFacade.GetAll();
            DriverShareRides.AddRange(shareRides);
        }

        private async void LoadShareRides(LoadUserProfile obj)
        {
            _userGuid = obj.Id;
            DriverShareRides.Clear();
            PassengerShareRides.Clear();

            var driverShareRides = await _shareRideFacade.GetDriverShareRides(obj.Id);
            var passengerShareRides = await _shareRideFacade.GetUserPassengerShareRides(obj.Id);

            DriverShareRides.AddRange(driverShareRides);
            PassengerShareRides.AddRange(passengerShareRides);
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

        private void OnOpenProfile(OpenProfileInfoMessage obj)
        {
            IsVisible = false;
        }

        private void OnUserCarsOpen(OpenUserCarsMessage obj)
        {
            IsVisible = false;
        }

        private void OnUserRidesOpen(OpenUserShareRidesMessage obj)
        {
            IsVisible = true;
        }

        private void OnLogOut(LogOutMessage obj)
        {
            IsVisible = false;
        }
        private void OnHome(HomeMessage obj)
        {
            IsVisible = false;
        }

    }
}
