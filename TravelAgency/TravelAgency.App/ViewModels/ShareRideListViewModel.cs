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

namespace TravelAgency.App.ViewModels
{
    public class ShareRideListViewModel : ViewModelBase, IShareRideListViewModel
    {

        private readonly ShareRideFacade _shareRideFacade;
        private readonly IMediator _mediator;



        public ShareRideListViewModel(
            ShareRideFacade shareRideFacade,
            IMediator mediator)
        {
            _shareRideFacade = shareRideFacade;
            _mediator = mediator;
            ShareRideSelectedCommand = new RelayCommand<ShareRideListModel>(ShareRideSelected);
            ShareRideNewCommand = new RelayCommand(ShareRideNew);

            mediator.Register<UpdateMessage<ShareRideWrapper>>(ShareRideUpdated);
        }

        public ObservableCollection<ShareRideListModel> ShareRides { get; set; } = new();


        public ICommand ShareRideSelectedCommand { get; }
        public ICommand ShareRideNewCommand { get; }

        private void ShareRideNew() => _mediator.Send(new NewMessage<ShareRideWrapper>());

        private void ShareRideSelected(ShareRideListModel? shareRides) => _mediator.Send(new SelectedMessage<ShareRideWrapper> { Id = shareRides?.Id });

        private async void ShareRideUpdated(UpdateMessage<ShareRideWrapper> _) => await LoadAsync();


        public async Task LoadAsync()
        {
            ShareRides.Clear();
            var shareRides = await _shareRideFacade.GetAll();
            ShareRides.AddRange(shareRides);

        }


    }
}
