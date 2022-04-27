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
        private readonly IFactory<IUserDetailViewModel> _userDetailViewModelFactory;
        private readonly IFactory<ICarDetailViewModel> _carDetailViewModelFactory;

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
                IProfileInfoViewModel profileInfoViewModel,
                IUserCarsViewModel userCarsViewModel,
                IUserRidesViewModel userRidesViewModel,
                ICreateRideViewModel createRideViewModel,
                IMediator mediator,
                IFactory<IUserDetailViewModel> userDetailViewModelFactory,
                IFactory<ICarDetailViewModel> carDetailViewModelFactory,
                IFactory<IShareRideDetailViewModel> shareRideDetailViewModelFactory)
            //IFactory<IPassengerOfShareRideDetailViewModel> passengerOfShareRideDetailViewModelFactory)//TODO
        {

            UserListViewModel = userListViewModel;
            CarListViewModel = carListViewModel;
            ShareRideListViewModel = shareRideListViewModel;
            PassengerOfShareRideListViewModel = passengerOfShareRideListViewModel;
            SelectOptionViewModel = selectOptionViewModel;
            ProfileWindowViewModel = profileWindowViewModel;
            ProfileInfoViewModel = profileInfoViewModel;
            UserCarsViewModel = userCarsViewModel;
            CreateRideViewModel = createRideViewModel;
            UserRidesViewModel = userRidesViewModel;
            _userDetailViewModelFactory = userDetailViewModelFactory;
            _carDetailViewModelFactory = carDetailViewModelFactory;
            _shareRideDetailViewModelFactory = shareRideDetailViewModelFactory;

            //_passengerOfShareRideDetailViewModelFactory = passengerOfShareRideDetailViewModelFactory;

            UserDetailViewModel = _userDetailViewModelFactory.Create();
            CarDetailViewModel = _carDetailViewModelFactory.Create();
            ShareRideDetailViewModel = _shareRideDetailViewModelFactory.Create();
            //PassengerOfShareRideDetailViewModel = _passengerOfShareRideDetailViewModelFactory.Create();
            _mediator = mediator;

            mediator.Register<SelectedMessage<UserWrapper>>(OnUserSelected);
            OpenProfile = new RelayCommand(OnProfileButtonClicked);
        }


        public IUserDetailViewModel? SelectedUserDetailViewModel { get; set; }

        public IUserListViewModel UserListViewModel { get; }
        public ICarListViewModel CarListViewModel { get; }
        public IShareRideListViewModel ShareRideListViewModel { get; }
        public IPassengerOfShareRideListViewModel PassengerOfShareRideListViewModel { get; }
        public ISelectOptionViewModel SelectOptionViewModel { get; }
        public IProfileWindowViewModel ProfileWindowViewModel { get; }
        public IProfileInfoViewModel ProfileInfoViewModel { get; }
        public IUserCarsViewModel UserCarsViewModel { get; }
        public IUserRidesViewModel UserRidesViewModel { get; }

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
            _mediator.Send(new OpenProfileInfoMessage());
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
                var userDetailViewModel = _userDetailViewModelFactory.Create();
                userDetailViewModel.LoadAsync(id.Value);

                SelectedUserDetailViewModel = userDetailViewModel;
            }
        }

    }
}
