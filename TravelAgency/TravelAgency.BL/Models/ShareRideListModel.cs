using AutoMapper;
using TravelAgency.DAL.Entities;

namespace TravelAgency.BL.Models
{
    public record ShareRideListModel(string FromPlace, string ToPlace, decimal Cost, DateTime LeaveTime) : ModelBase
    {
        public string FromPlace { get; set; } = FromPlace;
        public string ToPlace { get; set; } = ToPlace;
        public DateTime LeaveTime { get; set; } = LeaveTime;
        public decimal Cost { get; set; } = Cost;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ShareRideEntity, ShareRideListModel>()
                    .ReverseMap();
            }
        }
    }
}
