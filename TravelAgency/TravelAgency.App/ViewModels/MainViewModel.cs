using System;
using System.Windows;
using System.Windows.Input;
using TravelAgency.App.Commands;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Wrappers;

namespace TravelAgency.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        public ICommand OpenProfile { get; }
        public ICommand ExitApplication { get; set; }
        public ICommand MinimizeApplication { get; set; }
        public ICommand MaximizeApplication { get; set; }

        public MainViewModel(
                IUserListViewModel userListViewModel,
                ICarListViewModel carListViewModel,
                IShareRideListViewModel shareRideListViewModel,
                IPassengerOfShareRideListViewModel passengerOfShareRideListViewModel,
                ISelectOptionViewModel selectOptionViewModel,
                IProfileWindowViewModel profileWindowViewModel,
                ISearchRideViewModel searchRideViewModel,
                ICreateRideViewModel createRideViewModel,
                IFilteredRidesViewModel filteredRidesViewModel,
                IMediator mediator,
                IUserDetailViewModel userDetailViewModel,
                ICarDetailViewModel carDetailViewModel,
                IShareRideDetailViewModel shareRideDetailViewModel)
        {
            UserListViewModel = userListViewModel;
            UserDetailViewModel = userDetailViewModel;
            CarListViewModel = carListViewModel;
            CarDetailViewModel = carDetailViewModel;
            ShareRideListViewModel = shareRideListViewModel;
            ShareRideDetailViewModel = shareRideDetailViewModel;
            PassengerOfShareRideListViewModel = passengerOfShareRideListViewModel;
            SelectOptionViewModel = selectOptionViewModel;
            ProfileWindowViewModel = profileWindowViewModel;
            CreateRideViewModel = createRideViewModel;
            SearchRideViewModel = searchRideViewModel;
            FilteredRidesViewModel = filteredRidesViewModel;

            _mediator = mediator;

            ExitApplication = new RelayCommand(Exit);
            MinimizeApplication = new RelayCommand(Minimize);
            MaximizeApplication = new RelayCommand(Maximize);

            mediator.Register<SelectedMessage<UserWrapper>>(OnUserSelected);
            mediator.Register<NewMessage<UserWrapper>>(OnUserNewMessage);
            mediator.Register<SelectedMessage<CarWrapper>>(OnCarSelected);
            mediator.Register<NewMessage<CarWrapper>>(OnCarNewMessage);
            mediator.Register<DeleteMessage<CarWrapper>>(OnCarDeleteMessage);
            mediator.Register<UpdateMessage<CarWrapper>>(OnCarUpdateMessage);
            mediator.Register<SelectedMessage<ShareRideWrapper>>(OnShareRideSelected);
            mediator.Register<NewMessage<ShareRideWrapper>>(OnShareRideNewMessage);
            OpenProfile = new RelayCommand(OnProfileButtonClicked);
        }

        public IUserDetailViewModel? SelectedUserDetailViewModel { get; set; }
        public ICarDetailViewModel? SelectedCarDetailViewModel { get; set; }
        public IShareRideDetailViewModel? SelectedShareRideDetailViewModel { get; set; }

        public IUserListViewModel UserListViewModel { get; }
        public ICarListViewModel CarListViewModel { get; }
        public IShareRideListViewModel ShareRideListViewModel { get; }
        public IPassengerOfShareRideListViewModel PassengerOfShareRideListViewModel { get; }
        public ISelectOptionViewModel SelectOptionViewModel { get; }
        public IProfileWindowViewModel ProfileWindowViewModel { get; }
        public IFilteredRidesViewModel FilteredRidesViewModel { get; }
        public ISearchRideViewModel SearchRideViewModel { get; }
        public ICreateRideViewModel CreateRideViewModel { get; }
        public IUserDetailViewModel UserDetailViewModel { get; }
        public ICarDetailViewModel CarDetailViewModel { get; }
        public IShareRideDetailViewModel ShareRideDetailViewModel { get; }

        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Minimize()
        {
            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void Maximize()
        {
            System.Windows.Application.Current.MainWindow.WindowState = System.Windows.Application.Current.MainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        private void OnProfileButtonClicked()
        {
            // Load User Cars into Profile
            _mediator.Send(new LoadUserProfile(SelectedUserDetailViewModel.Model.Id));

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
                SelectedUserDetailViewModel ??= UserDetailViewModel;

                SelectedUserDetailViewModel.LoadAsync(id.Value);
            }
        }

        private void ReloadUser()
        {
            SelectedUserDetailViewModel?.LoadAsync(SelectedUserDetailViewModel.Model.Id);
        }

        private void OnCarDeleteMessage(DeleteMessage<CarWrapper> _)
        {
            ReloadUser();
        }

        private void OnCarUpdateMessage(UpdateMessage<CarWrapper> _)
        {
            ReloadUser();
        }

        private void OnCarNewMessage(NewMessage<CarWrapper> obj)
        {
            ReloadUser();
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
                SelectedCarDetailViewModel ??= CarDetailViewModel;

                SelectedCarDetailViewModel.LoadAsync(id.Value);
            }
        }

        private void OnShareRideNewMessage(NewMessage<ShareRideWrapper> obj)
        {
            SelectShareRide(Guid.Empty);
        }

        private void OnShareRideSelected(SelectedMessage<ShareRideWrapper> message)
        {
            SelectShareRide(message.Id);
        }

        private void SelectShareRide(Guid? id)
        {
            if (id is null)
            {
                SelectedShareRideDetailViewModel = null;
            }
            else
            {
                SelectedShareRideDetailViewModel ??= ShareRideDetailViewModel;

                SelectedShareRideDetailViewModel.LoadAsync(id.Value);
            }
        }

    }
}
