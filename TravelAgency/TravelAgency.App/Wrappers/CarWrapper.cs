using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.BL.Models;
using TravelAgency.Common.Enums;

namespace TravelAgency.App.Wrappers
{
    public class CarWrapper : ModelWrapper<CarDetailModel>
    {
        public CarWrapper(CarDetailModel model)
            : base(model)
        {
        }

        public string? LicensePlate
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? Manufacturer
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public CarType CarType
        {
            get => GetValue<CarType>();
            set => SetValue(value);
        }

        public DateTime RegistrationDate
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public string? ImgUrl
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public int Capacity
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public Guid OwnerId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        // TODO - constraints

        public static implicit operator CarWrapper(CarDetailModel detailModel)
            => new(detailModel);

        public static implicit operator CarDetailModel(CarWrapper wrapper)
            => wrapper.Model;
    }
}