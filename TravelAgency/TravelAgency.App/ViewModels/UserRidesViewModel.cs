using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelAgency.App.Services;
using TravelAgency.App.Messages;
using TravelAgency.App.Commands;

namespace TravelAgency.App.ViewModels
{
    public class UserRidesViewModel : ViewModelBase, IUserRidesViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;


        public UserRidesViewModel(IMediator mediator)
        {
            _mediator = mediator;

            mediator.Register<OpenUserCarsMessage>(OnUserCarsOpen);
            mediator.Register<OpenUserShareRidesMessage>(OnUserRidesOpen);
            mediator.Register<OpenProfileInfoMessage>(OnOpenProfile);
            mediator.Register<LogOutMessage>(OnLogOut);
        }

        public bool IsVisible
        {
            get { return _isVisible; }

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
            IsVisible = false;
        }

        private void OnUserRidesOpen(OpenUserShareRidesMessage obj)
        {
            IsVisible = true;
        }

        private void OnLogOut(LogOutMessage obj)
        {
            IsVisible = false;
        }


    }
}