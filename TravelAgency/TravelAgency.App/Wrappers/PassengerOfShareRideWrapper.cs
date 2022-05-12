using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.BL.Models;

namespace TravelAgency.App.Wrappers
{
    public class PassengerOfShareRideWrapper : ModelWrapper<PassengerOfShareRideDetailModel>
    {
        public PassengerOfShareRideWrapper(PassengerOfShareRideDetailModel model)
            : base(model)
        {
        }

        public Guid PassengerId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public Guid ShareRideId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public static implicit operator PassengerOfShareRideWrapper(PassengerOfShareRideDetailModel detailModel)
            => new(detailModel);

        public static implicit operator PassengerOfShareRideDetailModel(PassengerOfShareRideWrapper wrapper)
            => wrapper.Model;
    }
}