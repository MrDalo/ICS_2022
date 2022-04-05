using System;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace TravelAgency.Common.Tests.Seeds
{
    public static class PassengerOFShareRideSeeds
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
            PassengerId: UserSeeds.PassengerTest.Id,
            ShareRideId: ShareRideSeeds.ShareRideEntity1.Id
        )
        {
            Passenger = UserSeeds.PassengerTest,
            ShareRide = ShareRideSeeds.ShareRideEntity1

        };


        public static readonly PassengerOfShareRideEntity PassengerOfShareRide2 = new(
            PassengerId: UserSeeds.Passenger1.Id,
            ShareRideId: ShareRideSeeds.ShareRideEntityUpdate.Id
        )
        {
            Passenger = UserSeeds.Passenger1,
            ShareRide = ShareRideSeeds.ShareRideEntityUpdate

        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassengerOfShareRideEntity>().HasData(
                PassengerOfShareRide1 with {Passenger = null, ShareRide = null},
                PassengerOfShareRide2 with { Passenger = null, ShareRide = null }
                
            );
        }
    }
}
