using System;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace TravelAgency.DAL.Seeds
{
    public static class PassengerOfShareRideSeeds
    {

        public static readonly PassengerOfShareRideEntity EmptyPassengerOfShareRideEntity = new(
            PassengerId: default,
            ShareRideId: default
        )
        {
            Passenger = default,
            ShareRide = default

        };


        public static readonly PassengerOfShareRideEntity PassengerOfShareRide1 = new(
            PassengerId: UserSeeds.Passenger1.Id,
            ShareRideId: ShareRideSeeds.ShareRide1.Id
        )
        {
            Passenger = UserSeeds.Passenger1,
            ShareRide = ShareRideSeeds.ShareRide1

        };


        public static readonly PassengerOfShareRideEntity PassengerOfShareRide2 = new(
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

