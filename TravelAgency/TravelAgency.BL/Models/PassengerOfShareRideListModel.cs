﻿using AutoMapper;
using TravelAgency.DAL.Entities;

namespace TravelAgency.BL.Models
{
    public record PassengerOfShareRideListModel(
        Guid PassengerId,
        Guid ShareRideId
        ) : ModelBase
    {
        public Guid PassengerId { get; set; } = PassengerId;
        public Guid ShareRideId { get; set; } = ShareRideId;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<PassengerOfShareRideEntity, PassengerOfShareRideListModel>()
                    .ReverseMap();
            }
        }
    }
}
