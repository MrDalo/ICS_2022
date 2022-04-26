using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ObservableCollection<ShareRideWrapper> DriverShareRides { get; set; } = new();

        public ObservableCollection<PassengerOfShareRideWrapper> PassengerShareRides { get; set; } = new();

        public ObservableCollection<CarWrapper> Cars { get; set; } = new();

        // TODO - constraints

        public static implicit operator UserWrapper(UserDetailModel detailModel)
            => new(detailModel);

        public static implicit operator UserDetailModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}
