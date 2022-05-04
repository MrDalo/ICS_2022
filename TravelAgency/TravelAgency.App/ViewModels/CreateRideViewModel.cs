using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Services;
using TravelAgency.BL.Facades;
using TravelAgency.BL.Models;
using TravelAgency.App.Messages;
using TravelAgency.App.Commands;
using TravelAgency.App.Extensions;
using TravelAgency.App.Wrappers;

namespace TravelAgency.App.ViewModels
{
    public class CreateRideViewModel : ViewModelBase, ICreateRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public ICommand SubmitCreation { get; }
        public ICommand GoBack { get; }

        private readonly CarFacade _carFacade;
        private readonly ShareRideFacade _shareRideFacade ;

        private Guid _idUser;


        private ShareRideWrapper? _model = new ShareRideDetailModel(string.Empty, string.Empty, default, default, ArriveTime: default, CarId: default, DriverId:Guid.Empty);

        public CreateRideViewModel(CarFacade carFacade, ShareRideFacade shareRideFacade, IMediator mediator)
        {
            _mediator = mediator;
            _carFacade = carFacade;
            _shareRideFacade = shareRideFacade;

            CarSelectedCommand = new RelayCommand<CarListModel>(CarSelected);

            mediator.Register<CreateRideWindowMessage>(CreateRideWindowOpen);

            SubmitCreation = new AsyncRelayCommand(SubmitCreationFunc);
            GoBack = new RelayCommand(GoBackFunc);

        }

        private void CarSelected(CarListModel? carListModel)
        {
            if (carListModel is not null)
            {
                _model.CarId = carListModel.Id;
            }
        }

        public ICommand CarSelectedCommand { get; }

        public ShareRideWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
            }
        }

        //public async Task LoadAsync(Guid id)
        //{
        //    Model = await _shareRideFacade.GetAsync(id) ?? new(string.Empty, string.Empty, default, default, ArriveTime: default, CarId: Guid.Empty, DriverId: id);
        //}

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CarListModel> Cars { get; set; } = new();



        public async Task SubmitCreationFunc()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            if (Model.DriverId == Guid.Empty)
            {
                _model.DriverId=_idUser; 
            }
            
            OnPropertyChanged();

            Model = await _shareRideFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<ShareRideWrapper> { Model = Model });

            Model = null;
            IsVisible = false;
        }

        private void GoBackFunc()
        {
            IsVisible = false;
        }

        private async Task FillCarsObservableCollection(Guid userId)
        {
            Cars.Clear();
            Cars.AddRange(await _carFacade.GetAllUserCars(userId));
            _idUser = userId;
        }

        private void CreateRideWindowOpen(CreateRideWindowMessage obj)
        {
            FillCarsObservableCollection(obj.userID);
            IsVisible = true;
        }

    }
}
