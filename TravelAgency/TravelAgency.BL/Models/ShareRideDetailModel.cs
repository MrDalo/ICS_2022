﻿using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;


namespace TravelAgency.BL.Models
{
    public record ShareRideDetailModel(string FromPlace, string ToPlace, decimal Cost, DateTime LeaveTime, DateTime ArriveTime, Guid CarId, Guid DriverId) : ModelBase
    {
        public string FromPlace { get; set; } = FromPlace;
        public string ToPlace { get; set; } = ToPlace;
        public DateTime LeaveTime { get; set; } = LeaveTime;
        public DateTime ArriveTime { get; set; } = ArriveTime;
        public decimal Cost { get; set; } = Cost;
        public Guid CarId { get; set; } = CarId;
        public Guid DriverId { get; set; } = DriverId;


        public List<UserListModel> Passengers { get; init; } = new();

        

        //public CarEntity? Car { get; init; }

        //public UserEntity? Driver { get; init; }
        

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ShareRideEntity, ShareRideDetailModel>()
                    .ReverseMap();
            }
        }
    }
}
