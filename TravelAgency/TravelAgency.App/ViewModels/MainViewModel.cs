using TravelAgency.App.Factories;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TravelAgency.App.Commands;

namespace TravelAgency.App.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private readonly IFactory<IShareRideDetailViewModel> _shareRideDetailViewModelFactory;
        //private readonly IFactory<IPassengerOfShareRideDetailViewModel> _passengerOfShareRideDetailViewModelFactory;

        private readonly IMediator _mediator;
        public ICommand OpenProfile { get; }

        public MainViewModel(
                IUserListViewModel userListViewModel,
                ICarListViewModel carListViewModel,
                IShareRideListViewModel shareRideListViewModel,
                IPassengerOfShareRideListViewModel passengerOfShareRideListViewModel,
                ISelectOptionViewModel selectOptionViewModel,
                IProfileWindowViewModel profileWindowViewModel,
                IUserRidesViewModel userRidesViewModel,
                ISearchRideViewModel searchRideViewModel,
                ICreateRideViewModel createRideViewModel,
                IMediator mediator,
                IUserDetailViewModel userDetailViewModel,
                ICarDetailViewModel carDetailViewModel,
                IFactory<IShareRideDetailViewModel> shareRideDetailViewModelFactory)
                //IFactory<IPassengerOfShareRideDetailViewModel> passengerOfShareRideDetailViewModelFactory)//TODO
        {
            UserListViewModel = userListViewModel;
            UserDetailViewModel = userDetailViewModel;
            CarListViewModel = carListViewModel;
            CarDetailViewModel = carDetailViewModel;
            ShareRideListViewModel = shareRideListViewModel;
            PassengerOfShareRideListViewModel = passengerOfShareRideListViewModel;
            SelectOptionViewModel = selectOptionViewModel;
            ProfileWindowViewModel = profileWindowViewModel;
            CreateRideViewModel = createRideViewModel;
            UserRidesViewModel = userRidesViewModel;
            SearchRideViewModel = searchRideViewModel;

            _shareRideDetailViewModelFactory = shareRideDetailViewModelFactory;
            //_passengerOfShareRideDetailViewModelFactory = passengerOfShareRideDetailViewModelFactory;

            ShareRideDetailViewModel = _shareRideDetailViewModelFactory.Create();
            //PassengerOfShareRideDetailViewModel = _passengerOfShareRideDetailViewModelFactory.Create();
            _mediator = mediator;

            mediator.Register<SelectedMessage<UserWrapper>>(OnUserSelected);
            mediator.Register<NewMessage<UserWrapper>>(OnUserNewMessage);
            mediator.Register<SelectedMessage<CarWrapper>>(OnCarSelected);
            mediator.Register<NewMessage<CarWrapper>>(OnCarNewMessage);
            OpenProfile = new RelayCommand(OnProfileButtonClicked);
        }

        public IUserDetailViewModel? SelectedUserDetailViewModel { get; set; }
        public ICarDetailViewModel? SelectedCarDetailViewModel { get; set; }

        public IUserListViewModel UserListViewModel { get; }
        public ICarListViewModel CarListViewModel { get; }
        public IShareRideListViewModel ShareRideListViewModel { get; }
        public IPassengerOfShareRideListViewModel PassengerOfShareRideListViewModel { get; }
        public ISelectOptionViewModel SelectOptionViewModel { get; }
        public IProfileWindowViewModel ProfileWindowViewModel { get; }
        public IUserRidesViewModel UserRidesViewModel { get; }
        public ISearchRideViewModel SearchRideViewModel { get; }
        public ICreateRideViewModel CreateRideViewModel { get; }

        public IUserDetailViewModel UserDetailViewModel { get; }
        public ICarDetailViewModel CarDetailViewModel { get; }
        public IShareRideDetailViewModel ShareRideDetailViewModel { get; }
        //public IPassengerOfShareRideDetailViewModel PassengerOfShareRideDetailViewModel { get; }

        public ObservableCollection<IUserDetailViewModel> UserDetailViewModels { get; } = new ObservableCollection<IUserDetailViewModel>();

        public ObservableCollection<IShareRideDetailViewModel> ShareRideDetailViewModels { get; } = new ObservableCollection<IShareRideDetailViewModel>();
       // public ObservableCollection<IPassengerOfShareRideDetailViewModel> PassengerOfShareRideDetailViewModels { get; } = new ObservableCollection<IPassengerOfShareRideDetailViewModel>();
        public ObservableCollection<ICarDetailViewModel> CarViewModels { get; } = new ObservableCollection<ICarDetailViewModel>();

        private void OnProfileButtonClicked()
        {
            // Load User Cars into Profile
            _mediator.Send(new LoadUserCarsMessage(SelectedUserDetailViewModel.Model.Id));
            
            // Profile Visibility
            _mediator.Send(new OpenProfileInfoMessage());
        }

        private void OnUserNewMessage(NewMessage<UserWrapper> obj)
        {
            SelectUser(Guid.Empty);
        }

        private void OnUserSelected(SelectedMessage<UserWrapper> message)
        {
            SelectUser(message.Id);
        }

        private void SelectUser(Guid? id)
        {
            if (id is null)
            {
                SelectedUserDetailViewModel = null;
            }

            else
            {
                if (SelectedUserDetailViewModel == null)
                {
                    SelectedUserDetailViewModel = UserDetailViewModel;
                }

                SelectedUserDetailViewModel.LoadAsync(id.Value);
            }
        }

        private void OnCarNewMessage(NewMessage<CarWrapper> obj)
        {
            SelectCar(Guid.Empty);
        }

        private void OnCarSelected(SelectedMessage<CarWrapper> message)
        {
            SelectCar(message.Id);
        }

        private void SelectCar(Guid? id)
        {
            if (id is null)
            {
                SelectedCarDetailViewModel = null;
            }
            else
            {
                if (SelectedCarDetailViewModel == null)
                {
                    SelectedCarDetailViewModel = CarDetailViewModel;
                }

                SelectedCarDetailViewModel.LoadAsync(id.Value);
            }
        }

    }
}
