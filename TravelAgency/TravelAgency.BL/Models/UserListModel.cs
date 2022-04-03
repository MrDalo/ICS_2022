using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;



namespace TravelAgency.BL.Models
{

    //Neviem ci to vyvtvorit so vstupnym parametrom alebo nie

    //List bude sluzit na uvodnej page aplikaice na zobrazenie userov v drop-down liste
    // Mapovanie z entity na listModel
    //public record UserListModel() : ModelBase
    public record UserListModel(string Login) : ModelBase
    {
        //public string? Name { get; set; }

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
