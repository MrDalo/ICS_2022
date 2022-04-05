using System;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace TravelAgency.DAL.Seeds
{
    public static class PassengerOfShareRideSeeds
    {

        public static readonly PassengerOfShareRideEntity EmptyPassengerOfShareRideEntity = new(
            Id:default,
            PassengerId: default,
            ShareRideId: default
        )
        {
            Passenger = default,
            ShareRide = default

        };


        public static readonly PassengerOfShareRideEntity PassengerOfShareRide1 = new(
            Id: Guid.Parse(input: "789cffe3-6d03-4c93-b39a-b81711ebd81f"),
            PassengerId: UserSeeds.Passenger1.Id,
            ShareRideId: ShareRideSeeds.ShareRide1.Id
        )
        {
            Passenger = UserSeeds.Passenger1,
            ShareRide = ShareRideSeeds.ShareRide1

        };


        public static readonly PassengerOfShareRideEntity PassengerOfShareRide2 = new(
            Id: Guid.Parse(input: "07fc5ea5-6d96-40ec-9c24-7f1ac3ac180e"),
            PassengerId: UserSeeds.Passenger2.Id,
            ShareRideId: ShareRideSeeds.ShareRide1.Id
        )
        {
            Passenger = UserSeeds.Passenger2,
            ShareRide = ShareRideSeeds.ShareRide1

        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassengerOfShareRideEntity>().HasData(
                PassengerOfShareRide1 with { Passenger = null, ShareRide = null },
                PassengerOfShareRide2 with { Passenger = null, ShareRide = null }
                
            );
        }
    }
}

