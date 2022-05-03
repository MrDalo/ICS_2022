using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.App.Extensions;
using TravelAgency.BL.Models;

namespace TravelAgency.App.Wrappers
{
    public class UserWrapper : ModelWrapper<UserDetailModel>
    {
        public UserWrapper(UserDetailModel model)
            : base(model)
        {
        }

        public string? Login
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? Surname
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? Email
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? PhoneNumber
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? PhotoUrl 
        { 
            get => GetValue<string>(); 
            set => SetValue(value);
        }
        private void InitializeCollectionProperties(UserDetailModel model)
        {
            if (model.DriverShareRides == null)
            {
                throw new ArgumentException("Ingredients cannot be null");
            }
            DriverShareRides.AddRange(model.DriverShareRides.Select(e => new ShareRideWrapper(e)));

            RegisterCollection(DriverShareRides, model.DriverShareRides);

            if (model.PassengerShareRides == null)
            {
                throw new ArgumentException("Ingredients cannot be null");
            }
            PassengerShareRides.AddRange(model.PassengerShareRides.Select(e => new PassengerOfShareRideWrapper(e)));

            RegisterCollection(PassengerShareRides, model.PassengerShareRides);

            //if (model.Cars == null)
            //{
            //    throw new ArgumentException("Ingredients cannot be null");
            //}
            //Cars.AddRange(model.Cars.Select(e => new CarWrapper(e)));

            //RegisterCollection(Cars, model.Cars);
        }

        public ObservableCollection<ShareRideWrapper> DriverShareRides { get; set; } = new();

        public ObservableCollection<PassengerOfShareRideWrapper> PassengerShareRides { get; set; } = new();

        public ObservableCollection<CarWrapper> Cars { get; set; } = new();

        // TODO - constraints

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                yield return new ValidationResult($"{nameof(Login)} is required", new[] { nameof(Login) });
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult($"{nameof(Name)} is required", new[] { nameof(Name) });
            }

            if (string.IsNullOrWhiteSpace(Surname))
            {
                yield return new ValidationResult($"{nameof(Surname)} is required", new[] { nameof(Surname) });
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult($"{nameof(Email)} is required", new[] { nameof(Email) });
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                yield return new ValidationResult($"{nameof(PhoneNumber)} is required", new[] { nameof(PhoneNumber) });
            }
        }

        public static implicit operator UserWrapper(UserDetailModel detailModel)
            => new(detailModel);

        public static implicit operator UserDetailModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}
