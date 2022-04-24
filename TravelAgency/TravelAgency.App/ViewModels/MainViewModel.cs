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
        //private readonly IFactory<IUserDetailViewModel> _userDetailViewModelFactory;
        //private readonly IFactory<ICarDetailViewModel> _carDetailViewModelFactory;
        //private readonly IFactory<IShareRideDetailViewModel> _shareRideDetailViewModelFactory;
        //private readonly IFactory<IPassengerOfShareRideDetailViewModel> _passengerOfShareRideDetailViewModelFactory;

        public MainViewModel(
            IUserListViewModel userListViewModel,
            //ICarListViewModel carListViewModel,
            //IShareRideListViewModel shareRideListViewModel,
            //IPassengerOfShareRideListViewModel passengerOfShareRideListViewModel,
            IMediator mediator)
            //IFactory<IUserDetailViewModel> userDetailViewModelFactory,
            //IFactory<ICarDetailViewModel> carDetailViewModelFactory,
            //IFactory<IShareRideDetailViewModel> shareRideDetailViewModelFactory,
            //IFactory<IPassengerOfShareRideDetailViewModel> passengerOfShareRideDetailViewModelFactory)//TODO
        {
            //TODO
            UserListViewModel = userListViewModel;
            //CarListViewModel = carListViewModel;
            //ShareRideListViewModel = shareRideListViewModel;
            //PassengerOfShareRideListViewModel = passengerOfShareRideListViewModel;
            //_userDetailViewModelFactory = userDetailViewModelFactory;
            //_carDetailViewModelFactory = carDetailViewModelFactory;
            //_shareRideDetailViewModelFactory = shareRideDetailViewModelFactory;
            //_passengerOfShareRideDetailViewModelFactory = passengerOfShareRideDetailViewModelFactory;


        }

        public IUserListViewModel UserListViewModel { get; }
        //public ICarListViewModel CarListViewModel { get; }
        //public IShareRideListViewModel ShareRideListViewModel { get; }
        //public IPassengerOfShareRideListViewModel PassengerOfShareRideListViewModel { get; }

        //public ObservableCollection<IUserDetailViewModel> UserDetailViewModels { get; } = new ObservableCollection<IUserDetailViewModel>();
    }
}
