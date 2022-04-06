using AutoMapper;
using TravelAgency.DAL.Entities;

namespace TravelAgency.BL.Models
{
    public record UserDetailModel(
        string Login,
        string Name,
        string Surname,
        string Email,
        string PhoneNumber) : ModelBase
    {

        public string Login { get; set; } = Login;
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string Email { get; set; } = Email;
        public string PhoneNumber { get; set; } = PhoneNumber;
        public string? PhotoUrl { get; set; }


        public List<ShareRideDetailModel> DriverShareRides { get; init; } = new();

        public List<PassengerOfShareRideDetailModel> PassengerShareRides { get; init; } = new();

        public List<CarListModel> Cars { get; init; } = new();

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.Login, expression => expression.Ignore());
            }
        }

        public static UserDetailModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty );
    }
}
