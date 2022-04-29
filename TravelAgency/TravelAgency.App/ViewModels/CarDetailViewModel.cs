using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Extensions;
using TravelAgency.BL.Models;
using TravelAgency.App.Wrappers;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using TravelAgency.Common.Enums;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Input;
using TravelAgency.App.Services.MessageDialog;



namespace TravelAgency.App.ViewModels
{
    public class CarDetailViewModel : ViewModelBase, ICarDetailViewModel
    {

        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;
        private CarWrapper? _model = new CarDetailModel(string.Empty, string.Empty, default, default, 0, Guid.Empty);

        public CarDetailViewModel(
            CarFacade carFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        //public CarWrapper? Model { get; private set;}

        public CarWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _carFacade.GetAsync(id) ?? new(string.Empty, string.Empty, default, default, 0, Guid.Empty);

        }

        public async Task SaveAsync()
        {

            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _carFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarWrapper> { Model = Model });
            Model = new CarDetailModel(string.Empty, string.Empty, default, default, 0, Guid.Empty);
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
                var delete = _messageDialogService.Show(
                    $"Delete",
                    $"Do you want to delete {Model?.LicensePlate}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    await _carFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.LicensePlate} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<CarWrapper>
                {
                    Model = Model
                });
            }

        }



    }
}
