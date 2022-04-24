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

namespace TravelAgency.App.ViewModels
{
    public class CarListViewModel : ViewModelBase, ICarListViewModel
    {
        private readonly CarFacade _carFacade;
        private readonly IMediator _mediator;


        public CarListViewModel(
            CarFacade carFacade,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _mediator = mediator;

            CarSelectedCommand = new RelayCommand<CarListModel>(CarSelected);
            CarNewCommand = new RelayCommand(CarNew);

           
            mediator.Register<DeleteMessage<CarWrapper>>(CarDeleted);
        }

        public ObservableCollection<CarListModel> Cars { get; set; } = new();

        public ICommand CarSelectedCommand { get; }
        public ICommand CarNewCommand { get; }

        private void CarNew() => _mediator.Send(new NewMessage<CarWrapper>());

        private void CarSelected(CarListModel? car) => _mediator.Send(new SelectedMessage<CarWrapper> { Id = car?.Id });
        
        private async void CarDeleted(DeleteMessage<CarWrapper> _) => await LoadAsync();



        public async Task LoadAsync()
        {
            Cars.Clear();
            var cars = await _carFacade.GetAll();
            Cars.AddRange(cars);

        }
    }
}
