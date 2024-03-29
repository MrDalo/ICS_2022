﻿using System;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Tests.Common.Seeds;

public static class ShareRideSeeds
{
    public static readonly ShareRideEntity EmptyShareRideEntity = new(
            Id: default,
            FromPlace: default!,
            ToPlace: default!,
            LeaveTime: default,
            ArriveTime: default,
            Cost:default,
            CarId: default,
            DriverId: default)
        {
            Car = default,
            Driver = default
        };

    public static readonly ShareRideEntity ShareRideEntity1 = new(
        Id: Guid.Parse(input: "C0EF1FBF-6F5D-41A2-AB30-D6915858D8FD"),
        FromPlace: "Bratislava",
        ToPlace: "Brno",
        LeaveTime: new DateTime(2022, 4, 12, 12, 30, 0),
        ArriveTime: new DateTime(2022, 4, 12, 14, 30, 0),
        Cost: 8,
        CarId: CarSeeds.CarEntity1.Id,
        DriverId: UserSeeds.UserEntity.Id)
    {
        Car = CarSeeds.CarEntity1,
        Driver = UserSeeds.UserEntity
    };
    

    public static readonly ShareRideEntity ShareRideToBeUpdated1 = new(
        Id: Guid.Parse(input: "7b479595-03a2-42a4-b47f-a421cb94b2d9"),
        FromPlace: "Ilava",
        ToPlace: "Brno",
        LeaveTime: new DateTime(2022, 4, 10, 11, 30, 0),
        ArriveTime: new DateTime(2022, 4, 10, 14, 30, 0),
        Cost: 6,
        CarId: CarSeeds.CarToBeUpdated.Id,
        DriverId: UserSeeds.PassengerTest69.Id)
    {
        Car = CarSeeds.CarToBeUpdated,
        Driver = UserSeeds.PassengerTest69
    };

    public static readonly ShareRideEntity ShareRideWithoutPass1 = new(
        Id: Guid.Parse(input: "20C97578-A2B7-4B46-991D-4DC0A29AE4B1"),
        FromPlace: "Ilava",
        ToPlace: "Viena",
        LeaveTime: new DateTime(2022, 6, 13, 14, 30, 0),
        ArriveTime: new DateTime(2022, 6, 13, 16, 30, 0),
        Cost: 6,
        CarId: CarSeeds.CarToBeUpdated.Id,
        DriverId: UserSeeds.DriverTest72.Id)
    {
        Car = CarSeeds.CarToBeUpdated,
        Driver = UserSeeds.DriverTest72
    };

    public static readonly ShareRideEntity ShareRideWithoutPass2 = new(
        Id: Guid.Parse(input: "5A1514DB-46DE-4BDD-A89C-4EA677966891"),
        FromPlace: "Ilava",
        ToPlace: "Viena",
        LeaveTime: new DateTime(2022, 6, 13, 14, 30, 0),
        ArriveTime: new DateTime(2022, 6, 13, 16, 30, 0),
        Cost: 6,
        CarId: CarSeeds.CarToBeUpdated.Id,
        DriverId: UserSeeds.DriverTest72.Id)
    {
        Car = CarSeeds.CarToBeUpdated,
        Driver = UserSeeds.DriverTest72
    };

    public static readonly ShareRideEntity ShareRideWithoutPass3 = new(
        Id: Guid.Parse(input: "6B059CC3-48CB-40F1-AD16-FA07AF25E739"),
        FromPlace: "Ilava",
        ToPlace: "Viena",
        LeaveTime: new DateTime(2022, 6, 13, 14, 30, 0),
        ArriveTime: new DateTime(2022, 6, 13, 16, 30, 0),
        Cost: 6,
        CarId: CarSeeds.CarToBeUpdated.Id,
        DriverId: UserSeeds.DriverTest72.Id)
    {
        Car = CarSeeds.CarToBeUpdated,
        Driver = UserSeeds.DriverTest72
    };

    public static readonly ShareRideEntity ShareRideToBeUpdated2 = new(
        Id: Guid.Parse(input: "e83ff5b2-9a73-459b-ac08-f71c1bf53968"),
        FromPlace: "Trencin",
        ToPlace: "Praha",
        LeaveTime: new DateTime(2022, 2, 12, 10, 30, 0),
        ArriveTime: new DateTime(2022, 2, 12, 14, 30, 0),
        Cost: 8,
        CarId: CarSeeds.CarToBeSearched.Id,
        DriverId: UserSeeds.PassengerTest69.Id)
    {
        Car = CarSeeds.CarToBeSearched,
        Driver = UserSeeds.PassengerTest69
    };


    public static readonly ShareRideEntity ShareRideEntityUpdate = ShareRideEntity1 with { Id = Guid.Parse(input: "111F1FBF-6F5D-41A2-AB30-D6915858D8FD"), Car = null, Driver = null, DriverId = UserSeeds.UserForShareRideUpdate.Id};
    public static readonly ShareRideEntity ShareRideEntityDelete = ShareRideEntity1 with { Id = Guid.Parse(input: "222F1FBF-6F5D-41A2-AB30-D6915858D8FD"), Car = null, Driver = null, DriverId = UserSeeds.UserForShareRideDelete.Id };
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShareRideEntity>().HasData(
            ShareRideEntity1 with { Car = null, Driver = null},
            ShareRideEntityUpdate with { Car = null, Driver = null },
            ShareRideEntityDelete with { Car = null, Driver = null },
            ShareRideToBeUpdated1 with { Car = null, Driver = null },
            ShareRideToBeUpdated2 with { Car = null, Driver = null },
            ShareRideWithoutPass1 with { Car = null, Driver = null },
            ShareRideWithoutPass2 with { Car = null, Driver = null },
            ShareRideWithoutPass3 with { Car = null, Driver = null }
        );
    }
}