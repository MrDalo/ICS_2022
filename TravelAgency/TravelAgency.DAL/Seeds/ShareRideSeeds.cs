using System;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Seeds;

public static class ShareRideSeeds
{
    public static readonly ShareRideEntity ShareRide1 = new(
        Id: Guid.Parse(input: "f3a11d15-d449-4632-8ba9-2720c0fdab70"),
        FromPlace: "Brno",
        ToPlace: "Prague",
        LeaveTime: new DateTime(2022, 2, 18, 12, 30, 0),
        ArriveTime: new DateTime(2022, 2, 18, 16, 30, 0),
        Cost: 10,
        CarId: CarSeeds.FiatMultipla.Id,
        DriverId: UserSeeds.Driver1.Id)
    {
        Car = CarSeeds.FiatMultipla,
        Driver = UserSeeds.Driver1,
    };

    static ShareRideSeeds()
    {
        ShareRide1.Passengers.Add(PassengerOfShareRideSeeds.PassengerOfShareRide1);
        ShareRide1.Passengers.Add(PassengerOfShareRideSeeds.PassengerOfShareRide2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShareRideEntity>().HasData(
            ShareRide1 with
            {
                Car = null,
                Driver = null,
                Passengers = Array.Empty<PassengerOfShareRideEntity>()
            }
        );
    }
}

