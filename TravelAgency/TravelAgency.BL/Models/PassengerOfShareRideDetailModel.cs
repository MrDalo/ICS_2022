using AutoMapper;
using TravelAgency.DAL.Entities;

namespace TravelAgency.BL.Models
{
    public record PassengerOfShareRideDetailModel(
        Guid PassengerId,
        Guid ShareRideId
        ) : ModelBase
    {
        public Guid PassengerId { get; set; } = PassengerId;
        public Guid ShareRideId { get; set; } = ShareRideId;

        public UserEntity? Passenger { get; set; }
        public ShareRideEntity? ShareRide { get; set; }


        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<PassengerOfShareRideEntity, PassengerOfShareRideDetailModel>()
                    .ReverseMap();
            }
        }
    }

}
