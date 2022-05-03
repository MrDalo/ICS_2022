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
using System;

namespace TravelAgency.App.ViewModels
{
    public class CarListViewModel : ViewModelBase, ICarListViewModel
    {
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public CarListViewModel(
            CarFacade carFacade,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _mediator = mediator;

            CarSelectedCommand = new RelayCommand<CarListModel>(CarSelected);
            CarNewCommand = new RelayCommand(CarNew);

            mediator.Register<OpenUserCarsMessage>(OnUserCarsOpen);
            mediator.Register<OpenUserShareRidesMessage>(OnUserRidesOpen);
            mediator.Register<OpenProfileInfoMessage>(OnOpenProfile);
            mediator.Register<LogOutMessage>(OnLogOut);
            mediator.Register<HomeMessage>(OnHome);

            mediator.Register<DeleteMessage<CarWrapper>>(CarDeleted);
            mediator.Register<UpdateMessage<CarWrapper>>(CarUpdated);

            mediator.Register<LoadUserProfile>(LoadCars);
        }
        
        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        private Guid _userGuid;
        public ICommand CarSelectedCommand { get; }
        public ICommand CarNewCommand { get; }

        private void CarNew() => _mediator.Send(new NewMessage<CarWrapper>());

        private void CarSelected(CarListModel? car) => _mediator.Send(new SelectedMessage<CarWrapper> { Id = car?.Id });
        
        private async void CarDeleted(DeleteMessage<CarWrapper> _) => await LoadAsync();

        private async void CarUpdated(UpdateMessage<CarWrapper> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Cars.Clear();
            var cars = await _carFacade.GetAllUserCars(_userGuid);
            Cars.AddRange(cars);
        }
        private async void LoadCars(LoadUserProfile obj)
        {
            _userGuid = obj.Id;
            Cars.Clear();
            var cars = await _carFacade.GetAllUserCars(obj.Id);
            Cars.AddRange(cars);
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
            IsVisible = true;
        }

        private void OnUserRidesOpen(OpenUserShareRidesMessage obj)
        {
            IsVisible = false;
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
