using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.Common.Enums;
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


        public List<ShareRideListModel> DriverShareRides { get; init; } = new();

        public List<ShareRideListModel> PassengerShareRides { get; init; } = new();

        public List<CarListModel> Cars { get; init; } = new();
        //TODO pri LIST Cars treba zistit a zvalidovat zapis, pretoze moze byt list prazdny, asi treba zmenit aj <CarEntity> na nejaky CarModel

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.Login, expression => expression.Ignore());
                        //Zabezpeci, ze uzivatel nemoze menit svoj login, //TODO treba sa dohodnut ci user moze zmenit svoj login - za mna nie lebo moze nastat problem pri ukladani ICollections
            }
        }

        public static UserDetailModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty );
    }
}
