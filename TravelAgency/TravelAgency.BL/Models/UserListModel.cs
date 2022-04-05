using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;



namespace TravelAgency.BL.Models
{
    public record UserListModel(string Login) : ModelBase
    {
        

        public string Login { get; set; } = Login;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserListModel>();
            }
        }
    }
}
