using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Extensions;
using TravelAgency.BL.Models;
using TravelAgency.App.Commands;
using TravelAgency.App.Wrappers;
using TravelAgency.BL.Facades;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TravelAgency.App.ViewModels
{
    public class PassengerOfShareRideListViewModel : ViewModelBase, IPassengerOfShareRideListViewModel
    {
        private readonly PassengerOfShareRideFacade _passengerOfShareRideFacade;
        private readonly IMediator _mediator;


        public PassengerOfShareRideListViewModel(
            PassengerOfShareRideFacade passengerOfShareRideFacade,
            IMediator mediator)
        {
            _passengerOfShareRideFacade = passengerOfShareRideFacade;
            _mediator = mediator;

            PassengerOfShareRideSelectedCommand = new RelayCommand<PassengerOfShareRideListModel>(PassengerOfShareRideSelected);
            PassengerOfShareRideNewCommand = new RelayCommand(PassengerOfShareRideNew);

            mediator.Register<UpdateMessage<PassengerOfShareRideWrapper>>(PassengerOfShareRideUpdated);
            mediator.Register<DeleteMessage<PassengerOfShareRideWrapper>>(PassengerOfShareRideDeleted);
        }


        public ObservableCollection<PassengerOfShareRideListModel> PassengerOfShareRides { get; set; } = new();

        public ICommand PassengerOfShareRideSelectedCommand { get; }
        public ICommand PassengerOfShareRideNewCommand { get; }

        private void PassengerOfShareRideNew() => _mediator.Send(new NewMessage<PassengerOfShareRideWrapper>());

        private void PassengerOfShareRideSelected(PassengerOfShareRideListModel? passengerOfShareRide) => _mediator.Send(new SelectedMessage<PassengerOfShareRideWrapper> { Id = passengerOfShareRide?.Id });

        
        private async void PassengerOfShareRideUpdated(UpdateMessage<PassengerOfShareRideWrapper> _) => await LoadAsync();

        private async void PassengerOfShareRideDeleted(DeleteMessage<PassengerOfShareRideWrapper> _) => await LoadAsync();


        public async Task LoadAsync()
        {
            PassengerOfShareRides.Clear();
            var passengerOfShareRides = await _passengerOfShareRideFacade.GetAll();
            PassengerOfShareRides.AddRange(passengerOfShareRides);

        }




    }
}
