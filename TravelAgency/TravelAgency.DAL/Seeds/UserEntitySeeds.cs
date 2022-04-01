using System;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Seeds;

public static class UserEntitySeeds
{
    public static readonly UserEntity Driver1 = new(
        Id: Guid.Parse(input: "dc5df605-d676-4c25-98d8-5b795c7b6503"),
        Name: "David",
        Login: "xsmith00",
        Surname: "Smith",
        Email: "davidsmith@gmail.com",
        PhoneNumber: "+421789564111",
        PhotoUrl: null)
    {
        // TODO: Add ICollections
    };

    public static readonly UserEntity Passenger1 = new(
        Id: Guid.Parse(input: "061f3338-0d39-4549-8b7d-ec74f76ba4b1"),
        Name: "Emanuel",
        Login: "xtaylo01",
        Surname: "Taylor",
        Email: "emanueltaylor@gmail.com",
        PhoneNumber: "+421045256789",
        PhotoUrl: null)
    {
        // TODO: Add ICollections
    };

    public static readonly UserEntity Passenger2 = new(
        Id: Guid.Parse(input: "05553338-0d39-4549-8b7d-ec74f76ba4b1"),
        Name: "Emily",
        Login: "xgreen12",
        Surname: "Green",
        Email: "emilygreen@gmail.com",
        PhoneNumber: "+421124569785",
        PhotoUrl: null)
    {
        // TODO: Add ICollections
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        // TODO: why is it set to null and empty
        modelBuilder.Entity<UserEntity>().HasData(
            Driver1 with
            {
                DriverShareRides = Array.Empty<ShareRideEntity>(),
                PassengerShareRides = Array.Empty<ShareRideEntity>(),
                Cars = Array.Empty<CarEntity>()
            },
            Passenger1 with
            {
                DriverShareRides = Array.Empty<ShareRideEntity>(),
                PassengerShareRides = Array.Empty<ShareRideEntity>(),
                Cars = Array.Empty<CarEntity>()
            },
            Passenger2 with
            {
                DriverShareRides = Array.Empty<ShareRideEntity>(),
                PassengerShareRides = Array.Empty<ShareRideEntity>(),
                Cars = Array.Empty<CarEntity>()
            }
        );
    }
}

