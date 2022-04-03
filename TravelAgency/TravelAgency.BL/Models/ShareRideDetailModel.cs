using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;


namespace TravelAgency.BL.Models
{
    public record ShareRideDetailModel(string FromPlace, string ToPlace, decimal Cost, DateTime LeaveTime, DateTime ArriveTime) : ModelBase
    {
        public string FromPlace { get; set; } = FromPlace;
        public string ToPlace { get; set; } = ToPlace;
        public DateTime LeaveTime { get; set; } = LeaveTime;
        public DateTime ArriveTime { get; set; } = ArriveTime;
        public decimal Cost { get; set; } = Cost;


        public List<UserListModel> Passengers { get; init; } = new();

        //Guid CarId,
        //Guid DriverId) : IEntity
        //{

        //public CarEntity? Car { get; init; }

        //public UserEntity? Driver { get; init; }
        

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ShareRideEntity, ShareRideDetailModel>();
            }
        }
    }
}
