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
    public class CreateRideViewModel : ViewModelBase, ICreateRideViewModel
    {
        private readonly IMediator _mediator;
        private bool _isVisible = false;

        public CreateRideViewModel(IMediator mediator)
        {
            _mediator = mediator;

            mediator.Register<ShareRideCreatingMessage>(OnShareRideCreating);
            mediator.Register<CreateRideWindowMessage>(CreateRideWindowOpen);


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


        private void OnShareRideCreating(ShareRideCreatingMessage obj)
        {
            IsVisible = false;

        }

        private void CreateRideWindowOpen(CreateRideWindowMessage obj)
        {
            IsVisible = true;

        }



    }
}
