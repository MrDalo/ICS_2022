using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.App.Extensions;
using TravelAgency.BL.Models;

namespace TravelAgency.App.Wrappers
{
    public class ShareRideWrapper : ModelWrapper<ShareRideDetailModel>
    {
        public ShareRideWrapper(ShareRideDetailModel model)
            : base(model)
        {
            InitializeCollectionProperties(model);
        }

        public string? FromPlace
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string? ToPlace
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime LeaveTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public DateTime ArriveTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        public decimal Cost
        {
            get => GetValue<decimal>();
            set => SetValue(value);
        }

        public Guid CarId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        public Guid DriverId
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }

        private void InitializeCollectionProperties(ShareRideDetailModel model)
        {
            if (model.Passengers == null)
            {
                throw new ArgumentException("Ingredients cannot be null");
            }
            Passengers.AddRange(model.Passengers.Select(e => new PassengerOfShareRideWrapper(e)));

            RegisterCollection(Passengers, model.Passengers);
        }

        public ObservableCollection<PassengerOfShareRideWrapper> Passengers { get; set; } = new();

        // TODO - constraints

        public static implicit operator ShareRideWrapper(ShareRideDetailModel detailModel)
            => new(detailModel);

        public static implicit operator ShareRideDetailModel(ShareRideWrapper wrapper)
            => wrapper.Model;
    }
}