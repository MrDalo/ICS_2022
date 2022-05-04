using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using System.Windows.Input;
using TravelAgency.App.Extensions;
using TravelAgency.App.Wrappers;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.BL.Models;

namespace TravelAgency.App.ViewModels
{
    public class ShareRideDetailViewModel : ViewModelBase, IShareRideDetailViewModel
    {
        private readonly ShareRideFacade _shareRideFacade;
        private readonly PassengerOfShareRideFacade _passengerOfShareRideFacade;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;
        private ShareRideWrapper? _model;

        private PassengerOfShareRideDetailModel? _selectedPassenger;

        public ShareRideDetailViewModel(
            ShareRideFacade shareRideFacade,
            PassengerOfShareRideFacade passengerOfShareRideFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _passengerOfShareRideFacade = passengerOfShareRideFacade;
            _mediator = mediator;
            _messageDialogService = messageDialogService;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            DeletePassengerCommand = new AsyncRelayCommand(DeletePassengerAsync);
            CloseCommand = new RelayCommand(Close);
        }
        public ObservableCollection<PassengerOfShareRideDetailModel> Passengers { get; set; } = new();

        public ShareRideWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
            }
        }
        public PassengerOfShareRideDetailModel? SelectedPassenger
        {
            get => _selectedPassenger;
            set
            {
                _selectedPassenger = value;
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand DeletePassengerCommand { get; }
        public ICommand CloseCommand { get; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _shareRideFacade.GetAsync(id) ?? new(string.Empty, string.Empty, 0, default, default, Guid.Empty, Guid.Empty);
            LoadPassengers();
        }

        public void LoadPassengers()
        {
            Passengers.Clear();
            Passengers.AddRange(Model.Model.Passengers);
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _shareRideFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<ShareRideWrapper> { Model = Model });
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                // TODO: error messages
                //var delete = _messageDialogService.Show(
                //    $"Delete",
                //    $"Do you want to delete {Model.ArriveTime}?.",
                //    MessageDialogButtonConfiguration.YesNo,
                //    MessageDialogResult.No);

                //if (delete == MessageDialogResult.No) return;

                try
                {
                    await _shareRideFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<ShareRideWrapper>
                {
                    Model = Model
                });
            }

            // Hide window
            Model = null;
        }

        public async Task DeletePassengerAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                // TODO: error messages
                //var delete = _messageDialogService.Show(
                //    $"Delete",
                //    $"Do you want to delete {Model.ArriveTime}?.",
                //    MessageDialogButtonConfiguration.YesNo,
                //    MessageDialogResult.No);

                //if (delete == MessageDialogResult.No) return;

                try
                {
                    await _passengerOfShareRideFacade.DeleteAsync(SelectedPassenger!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                // TODO osetrit
                Passengers.Remove(SelectedPassenger);
                
                // TODO update kolekcie
                //_mediator.Send(new DeleteMessage<ShareRideWrapper>
                //{
                //    Model = Model
                //});
            }

            // Hide window
            SelectedPassenger = null;
        }

        public void Close()
        {
            Model = null;
            _mediator.Send(new UpdateMessage<ShareRideWrapper> { Model = Model });
        }
    }
}
