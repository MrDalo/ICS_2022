using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity FiatMultipla = new(
        Id: Guid.Parse(input: "1206bea4-b4b2-41a1-bf70-4dc610283298"),
        LicensePlate: "PU584XG",
        Manufacturer: "Fiat",
        CarType: CarType.Other,
        ImgUrl: null,
        RegistrationDate: DateTime.Parse("2021-10-10"), 
        Capacity: 3,
        OwnerId: UserSeeds.Driver1.Id)
    {
        Owner = UserSeeds.Driver1
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
            FiatMultipla with { Owner = null }
        );
    }
}

