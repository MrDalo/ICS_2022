using System;
using TravelAgency.Common.Enums;
using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Common.Tests.Seeds;

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
    //public static readonly CarEntity CarEntityDelete = CarEntity1 with { Id = Guid.Parse("30872EFF-CED4-4F2B-89DB-0EE83A74D279"), Ingredient = null, Recipe = null, RecipeId = RecipeSeeds.RecipeForIngredientAmountEntityDelete.Id };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarEntity>().HasData(
         
            CarEntity1 with { Owner = null },
            CarEntity2 with { Owner = null },
            CarEntityUpdate,
            CarEntityUserContains
            /* IngredientAmountEntity2 with { Recipe = null, Ingredient = null },
            IngredientAmountEntityDelete*/
        );
    }
}