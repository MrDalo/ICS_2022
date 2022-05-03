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

namespace TravelAgency.App.ViewModels
{
    public class CreateRideViewModel : ViewModelBase, ICreateRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public ICommand SubmitCreation { get; }
        public ICommand GoBack { get; }

        private readonly CarFacade _carFacade;


        public CreateRideViewModel(CarFacade carFacade, IMediator mediator)
        {
            _mediator = mediator;
            _carFacade = carFacade;

            mediator.Register<CreateRideWindowMessage>(CreateRideWindowOpen);

            SubmitCreation = new RelayCommand(SubmitCreationFunc);
            GoBack = new RelayCommand(GoBackFunc);

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

        public string? FromPlace { get; set; }
        public string? ToPlace { get; set; }


        public ObservableCollection<CarListModel> Cars { get; set; } = new();



        private void SubmitCreationFunc()
        {
            //TODO - treba dorobit telo funkcie, vytvorenie shareRide a aj poriesit parameter funkcie (zvolene auto)

            IsVisible = false;

        }

        private void GoBackFunc()
        {
            IsVisible = false;

        }

        private async Task FillCarsObservableCollection(Guid userId)
        {
            Cars.AddRange(await _carFacade.GetAllUserCars(userId));
        }

        private void CreateRideWindowOpen(CreateRideWindowMessage obj)
        {
            FillCarsObservableCollection(obj.userID);
            IsVisible = true;

        }



    }
}
