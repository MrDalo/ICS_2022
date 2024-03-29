﻿using System;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Tests.Common.Seeds;

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
        Id: Guid.Parse(input: "4c5df605-d676-4c25-98d8-5b795c7b6503"),
        Name: "Johan",
        Login: "xsmith00",
        Surname: "Schnitt",
        Email: "davidsmith@gmail.com",
        PhoneNumber: "+421789564111",
        PhotoUrl: null
    );

    public static readonly UserEntity Passenger1 = new(
        Id: Guid.Parse(input: "5c5df605-d676-4c25-98d8-5b795c7b6503"),
        Login: "xsmith00",
        Name: "Lacko",
        Surname: "Placko",
        Email: "vut@gmail.com",
        PhoneNumber: "0949866579",
        PhotoUrl: null
    );

    public static readonly UserEntity PassengerTest = new(
        Id: Guid.Parse(input: "5c123605-d676-1234-98d8-5b795c7b6503"),
        Login: "xsmithasdf00",
        Name: "Maros",
        Surname: "Placasdfko",
        Email: "vut@gmail.com",
        PhoneNumber: "0949866579",
        PhotoUrl: null
    );

    public static readonly UserEntity PassengerTest2 = new(
        Id: Guid.Parse(input: "12313305-d676-1234-98d8-5b795c7b6503"),
        Login: "xsmithasdf00",
        Name: "Marosko",
        Surname: "Zajac",
        Email: "vut@gmail.com",
        PhoneNumber: "0949866579",
        PhotoUrl: null
    );


    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserEntityWithNoCars = 
        UserEntity with { Id = Guid.Parse("1117F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"), DriverShareRides = Array.Empty<ShareRideEntity>() , PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>()};
   
    public static readonly UserEntity UserEntityUpdate = UserEntity with { Id = Guid.Parse("2223F3CE-7B1A-48C1-9796-D2BAC7F67868"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserForCarEntityUpdate = UserEntity with { Id = Guid.Parse("333824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserCarEntityDelete = UserEntity with { Id = Guid.Parse("444824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserCarAdd = UserEntity with { Id = Guid.Parse("555824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserForUserCarDelete = UserEntity with { Id = Guid.Parse("666824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserForShareRideUpdate = UserEntity with { Id = Guid.Parse("777824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };
    public static readonly UserEntity UserForShareRideDelete = UserEntity with { Id = Guid.Parse("888824C0-A7D1-48BA-8E7C-4F136CF8BF31"), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() };



    public static readonly UserEntity PassengerTest69 = new(
        Id: Guid.Parse(input: "e11a73a4-db74-4749-a93d-6de07d773304"),
        Login: "xjozefs00",
        Name: "Jozef",
        Surname: "Skrecok",
        Email: "vut@gmail.com",
        PhoneNumber: "+421949866579",
        PhotoUrl: null
    );

    public static readonly UserEntity PassengerTest70 = new(
        Id: Guid.Parse(input: "222efa30-3bae-4bf6-97f0-793d6d08517d"),
        Login: "xkrali00",
        Name: "Marek",
        Surname: "Kralik",
        Email: "vut@gmail.com",
        PhoneNumber: "+421949866589",
        PhotoUrl: null
    );

    public static readonly UserEntity PassengerTest71 = new(
        Id: Guid.Parse(input: "9DFD52D1-F297-4431-BF60-02AAA268ADF6"),
        Login: "xkulla00",
        Name: "Parek",
        Surname: "Marek",
        Email: "vut@gmail.com",
        PhoneNumber: "+421949866589",
        PhotoUrl: null
    );

    public static readonly UserEntity DriverTest72 = new(
        Id: Guid.Parse(input: "A49CC5CE-84CA-4E50-8730-1E229C9A96B7"),
        Login: "xpekar00",
        Name: "Majko",
        Surname: "Gorosi",
        Email: "vut@gmail.com",
        PhoneNumber: "+421949866589",
        PhotoUrl: null
    );


    static UserSeeds()
    {
       UserEntity.Cars.Add(CarSeeds.CarEntity1);
       UserEntity.Cars.Add(CarSeeds.CarEntity2);
       UserEntity.DriverShareRides.Add(ShareRideSeeds.ShareRideEntity1);
       Passenger1.PassengerShareRides.Add(PassengerOfShareRideSeeds.PassengerOfShareRide2);
       PassengerTest.Cars.Add(CarSeeds.CarTest1);
       PassengerTest.Cars.Add(CarSeeds.CarTest2);
       PassengerTest2.Cars.Add(CarSeeds.CarTest3);
       PassengerTest2.Cars.Add(CarSeeds.CarTest4);
       PassengerTest69.Cars.Add(CarSeeds.CarToBeSearched);
       PassengerTest69.Cars.Add(CarSeeds.CarToBeDeleted);
       PassengerTest70.Cars.Add(CarSeeds.CarToBeUpdated);
       PassengerTest70.Cars.Add(CarSeeds.CarTest5);
       PassengerTest70.Cars.Add(CarSeeds.CarTest6);

    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            Passenger1 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            PassengerTest with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            PassengerTest2 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            UserEntityWithNoCars,
            UserEntityUpdate,
            UserForCarEntityUpdate,
            UserCarEntityDelete,
            UserForUserCarDelete,
            UserCarAdd,
            UserForShareRideUpdate,
            UserForShareRideDelete,
            DriverTest72 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            PassengerTest71 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            PassengerTest69 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() },
            PassengerTest70 with { DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(), Cars = Array.Empty<CarEntity>() }
        );
    }
}