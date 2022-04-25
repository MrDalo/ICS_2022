using System;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using System.Windows.Input;
using TravelAgency.App.Wrappers;
using TravelAgency.App.Services.MessageDialog;

namespace TravelAgency.App.ViewModels
{
    public class ShareRideDetailViewModel : ViewModelBase, IShareRideDetailViewModel
    {
        private readonly ShareRideFacade _shareRideFacade;
        private readonly IMediator _mediator;


        public ShareRideDetailViewModel(
            ShareRideFacade shareRideFacade,
            IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }


        public ShareRideWrapper? Model { get; private set; }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _shareRideFacade.GetAsync(id) ?? new(string.Empty, string.Empty, 0, default, default, Guid.Empty, Guid.Empty);
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

            _mediator.Send(new DeleteMessage<ShareRideWrapper>
            {
                Model = Model
            });

        }


    }
}
