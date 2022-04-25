using System;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.BL.Models;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using System.Windows.Input;
using TravelAgency.App.Wrappers;
using TravelAgency.App.Services.MessageDialog;


namespace TravelAgency.App.ViewModels
{
    public class PassengerOfShareRideDetailViewModel : ViewModelBase, IPassengerOfShareRideDetailViewModel
    {


        private readonly PassengerOfShareRideFacade _passengerOfShareRideFacade;
        private readonly IMediator _mediator;


        public PassengerOfShareRideDetailViewModel(
            PassengerOfShareRideFacade passengerOfShareRideFacade,
            IMediator mediator)
        {
            _passengerOfShareRideFacade = passengerOfShareRideFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        public PassengerOfShareRideWrapper? Model { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _passengerOfShareRideFacade.GetAsync(id) ?? new(Guid.Empty, Guid.Empty);
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _passengerOfShareRideFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<PassengerOfShareRideWrapper> { Model = Model });


        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            _mediator.Send(new DeleteMessage<PassengerOfShareRideWrapper>
            {
                Model = Model
            });

        }

    }
}
