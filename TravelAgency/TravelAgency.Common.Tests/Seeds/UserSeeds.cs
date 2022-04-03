using System;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace TravelAgency.Common.Tests.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity EmptyUserEntity = new(
        Id: default,
        Login: default!,
        Name: default!,
        Surname: default!,
        Email: default!,
        PhoneNumber: default!,
        PhotoUrl: default
    );

    public static readonly UserEntity UserEntity = new(
        Id: Guid.Parse(input: "dc5df605-d676-4c25-98d8-5b795c7b6503"),
        Name: "David",
        Login: "xsmith00",
        Surname: "Smith",
        Email: "davidsmith@gmail.com",
        PhoneNumber: "+421789564111",
        PhotoUrl: null
    );

    public static readonly UserEntity UserEntity1 = new(
        Id: Guid.Parse(input: "bc5df605-d676-4c25-98d8-5b795c7b6503"),
        Login: "xsmith00",
        Name: "Lacko",
        Surname: "Placko",
        Email: "vut@gmail.com",
        PhoneNumber: "0949866579",
        PhotoUrl: null
    );


    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserEntityWithNoCars = 
        UserEntity with { Id = Guid.Parse("98B7F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"), DriverShareRides = Array.Empty<ShareRideEntity>() , PassengerShareRides = Array.Empty<ShareRideEntity>(), Cars = Array.Empty<CarEntity>()};
   
    public static readonly UserEntity UserEntityUpdate = UserEntity with { /*Id = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"),*/ DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<ShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserForCarEntityUpdate = UserEntity with { Id = Guid.Parse("4FD824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<ShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
   
   
    //public static readonly UserEntity UserEntityDelete = UserEntity1 with { Id = Guid.Parse("dvc5df605-d676-4c25-98d8-5b795c7b6503"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<ShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    //TODO minimalne to uprac ty prase xD
    /*public static readonly UserEntity RecipeForIngredientAmountEntityDelete = UserEntity with { Id = Guid.Parse("F78ED923-E094-4016-9045-3F5BB7F2EB88"), Ingredients = Array.Empty<IngredientAmountEntity>() };*/


    static UserSeeds()
    {
       UserEntity.Cars.Add(CarSeeds.CarEntity2);
       
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<ShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            UserEntity1 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<ShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            UserEntityWithNoCars,
            UserEntityUpdate,
            UserForCarEntityUpdate
           // UserEntityDelete
        );
    }
}