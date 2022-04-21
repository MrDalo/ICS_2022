using System;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Tests.Common.Seeds;

public static class CarSeeds
{
    public static readonly CarEntity EmptyCarEntity = new(
        Id: default,
        LicensePlate: default!,
        Manufacturer: default!,
        CarType: default!,
        ImgUrl: default,
        RegistrationDate: default,
        Capacity: default,
        OwnerId: default)
    {
        Owner = default
    };

    public static readonly CarEntity CarEntity1 = new(
        Id: Guid.Parse(input: "1206bea4-b4b2-41a1-bf70-4dc610283298"),
        LicensePlate: "IL584XG",
        Manufacturer: "Skoda",
        CarType: CarType.Other,
        ImgUrl: null,
        RegistrationDate: DateTime.Parse("2021-10-10"),
        Capacity: 3,
        OwnerId: UserSeeds.UserEntity.Id)
    {
        Owner = UserSeeds.UserEntity
    };

    public static readonly CarEntity CarEntity2 = new(
        Id: Guid.Parse(input: "2226bea4-b4b2-41a1-bf70-46c610283298"),
        LicensePlate: "PU584XG",
        Manufacturer: "Fiat",
        CarType: CarType.Other,
        ImgUrl: null,
        RegistrationDate: DateTime.Parse("2021-10-10"),
        Capacity: 4,
        OwnerId: UserSeeds.UserEntity.Id)
    {
        Owner = UserSeeds.UserEntity
    };



    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly CarEntity CarEntityUpdate = CarEntity1 with { Id = Guid.Parse("A2E6849D-A158-4436-980C-7FC26B60C674"), Owner = null, OwnerId = UserSeeds.UserForCarEntityUpdate.Id };
    public static readonly CarEntity CarEntityUserContains = CarEntity1 with { Id = Guid.Parse("735FAEE9-4DCC-4B09-BD29-1BB60068DB34"), Owner = null, OwnerId = UserSeeds.UserForUserCarDelete.Id };
    public static readonly CarEntity CarEntityDelete = CarEntity1 with { Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"), Owner = null, OwnerId = UserSeeds.UserForUserCarDelete.Id };
    public static readonly CarEntity CarTest1 = CarEntity1 with { Id = Guid.Parse("1111131F-CED4-4F2B-89DB-0EE83A74D279"), Owner = null, OwnerId = UserSeeds.PassengerTest.Id };
    public static readonly CarEntity CarTest2 = CarEntity1 with { Id = Guid.Parse("2222231F-CED4-4F2B-89DB-0EE83111D279"), Owner = null, OwnerId = UserSeeds.PassengerTest.Id };
    public static readonly CarEntity CarTest3 = CarEntity1 with { Id = Guid.Parse("1121331F-CED4-4F2B-89DB-0EE83A74D279"), Owner = null, OwnerId = UserSeeds.PassengerTest2.Id };
    public static readonly CarEntity CarTest4 = CarEntity1 with { Id = Guid.Parse("2345731F-CED4-4F2B-89DB-0EE83111D279"), Owner = null, OwnerId = UserSeeds.PassengerTest2.Id };
    public static readonly CarEntity CarTest5 = CarEntity1 with { Id = Guid.Parse("61FCC4B4-A013-4876-815A-8DBFAA2C09E6"), Owner = null, OwnerId = UserSeeds.PassengerTest2.Id };
    public static readonly CarEntity CarTest6 = CarEntity1 with { Id = Guid.Parse("805E7434-7C7D-4816-938F-4B0CAB5347A7"), Owner = null, OwnerId = UserSeeds.PassengerTest2.Id };


    public static readonly CarEntity CarToBeSearched = EmptyCarEntity with
    {
        Id = Guid.Parse("eb3f858c-a6f3-4239-bc5f-934f5523c768"),
        LicensePlate= "BA000OS",
        Manufacturer= "Suzuki",
        CarType= CarType.Hatchback,
        ImgUrl= null,
        RegistrationDate= DateTime.Parse("2021-10-10"),
        Capacity= 3,
        Owner = null,
        OwnerId = UserSeeds.PassengerTest69.Id
    };
    
    public static readonly CarEntity CarToBeUpdated = EmptyCarEntity with
    {
        Id = Guid.Parse("6e223ea6-5f9b-4bee-88fb-2d79035021f9"),
        LicensePlate = "TN400XG",
        Manufacturer = "Volkswagen",
        CarType = CarType.Minivan,
        ImgUrl = null,
        RegistrationDate = DateTime.Parse("2008-05-10"),
        Capacity = 3,
        Owner = null, 
        OwnerId = UserSeeds.PassengerTest69.Id
    };

    public static readonly CarEntity CarToBeDeleted = EmptyCarEntity with
    {
        Id = Guid.Parse("3dbaf159-e8da-49e0-a86a-2aec748a5ac7"),
        LicensePlate = "PD666PU",
        Manufacturer = "Masserati",
        CarType = CarType.Sedan,
        ImgUrl = null,
        RegistrationDate = DateTime.Parse("2018-01-01"),
        Capacity = 3,
        Owner = null, 
        OwnerId = UserSeeds.PassengerTest70.Id
    };


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
         
            CarEntity1 with { Owner = null },
            CarEntity2 with { Owner = null },
            CarTest1,
            CarTest2,
            CarTest3,
            CarTest4,
            CarTest5,
            CarTest6,
            CarEntityUpdate,
            CarEntityUserContains,
            CarEntityDelete,
            CarToBeSearched,
            CarToBeDeleted,
            CarToBeUpdated
        );
    }
}