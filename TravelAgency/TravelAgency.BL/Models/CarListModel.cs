using AutoMapper;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;
using TravelAgency.Common.Enums;

namespace TravelAgency.BL.Models
{
    public record CarListModel(
        string LicensePlate,
        string Manufacturer,
        CarType CarType,
        DateTime RegistrationDate,
        int Capacity) : ModelBase
    {

        public string LicensePlate { get; set; } = LicensePlate;
        public string Manufacturer { get; set; } = Manufacturer;
        public CarType CarType { get; set; } = CarType;
        public DateTime RegistrationDate { get; set; } = RegistrationDate;
        public string? ImgUrl { get; set; }
        public int Capacity { get; set; } = Capacity;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarEntity, CarListModel>();
            }
        }
    }
}