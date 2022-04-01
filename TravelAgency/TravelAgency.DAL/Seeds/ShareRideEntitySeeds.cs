using System;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Seeds;

public static class ShareRideEntitySeeds
{
    public static readonly ShareRideEntity ShareRide1 = new(
        Id: Guid.Parse(input: "f3a11d15-d449-4632-8ba9-2720c0fdab70"),
        FromPlace: "Brno",
        ToPlace: "Prague",
        LeaveTime: DateTime.ParseExact("2022-02-18 05:50 PM", "yyyy-MM-dd HH:mm tt", null),
        ArriveTime: DateTime.ParseExact("2022-02-18 09:30 PM", "yyyy-MM-dd HH:mm tt", null),
        Cost: 10,
        CarId: CarEntitySeeds.FiatMultipla.Id,
        DriverId: UserEntitySeeds.Driver1.Id)
    {
        Car = CarEntitySeeds.FiatMultipla,
        Driver = UserEntitySeeds.Driver1,
        //Passengers = new List<UserEntity>{UserEntitySeeds.Passenger1}
    };

    // TODO:
    //static ShareRideSeeds()
    //{
    //    ShareRide1.Passengers.Add(UserEntitySeeds.Passenger1);
    //}

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShareRideEntity>().HasData(
            ShareRide1 with
            {
                Car = null,
                Driver = null,
                Passengers = Array.Empty<UserEntity>()
            }
        );
    }
}

