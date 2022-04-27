using System;
using System.Threading.Tasks;
using TravelAgency.App.Messages;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.BL.Models;
using TravelAgency.App.Commands;
using TravelAgency.BL.Facades;
using TravelAgency.App.Wrappers;
using System.Windows.Input;

namespace TravelAgency.App.ViewModels
{
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        private bool _isVisible = false;
        private UserWrapper? _model = UserDetailModel.Empty;

        public UserDetailViewModel(
            UserFacade userFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            mediator.Register<RegisterMessage>(OnOpenRegistration);
        }

        private void OnOpenRegistration(RegisterMessage obj)
        {
            IsVisible = true;
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

        //public UserWrapper? Model
        //{
        //    get; private set ;
        //}

        public UserWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
                //IngredientAmountDetailViewModel.RecipeId = value?.Id ?? Guid.Empty;
            }
        }

        public ICommand SaveCommand { get; set; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UserDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            IsVisible = false;

            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper>{Model = Model});
            Model = UserDetailModel.Empty;
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            _mediator.Send(new DeleteMessage<UserWrapper>
            {
                Model = Model
            });
        }






    }
}
