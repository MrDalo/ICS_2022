using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TravelAgency.BL.Models;
using TravelAgency.BL.Tests;
using TravelAgency.BL.Facades;
using TravelAgency.Common.Tests;
using TravelAgency.Common.Tests.Seeds;

using Microsoft.EntityFrameworkCore;
using TravelAgency.Common.Enums;
using Xunit.Abstractions;
using Xunit;


namespace TravelAgency.BL.Tests
{
    public sealed class CarFacadeTests : CRUDFacadeTestsBase
    {
        private readonly CarFacade _carFacadeSUT;

        public CarFacadeTests(ITestOutputHelper output) : base(output)
        {
            _carFacadeSUT = new CarFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new CarDetailModel
            (
                LicensePlate: "TN025AB",
                Manufacturer: "Mercedes",
                CarType: CarType.Minivan,
                RegistrationDate: DateTime.Parse("2021-10-12"),
                Capacity:5
            );

            var returnedModel = await _carFacadeSUT.SaveAsync(model);
            Assert.Equal(model, returnedModel);
        }
        /*
        [Fact]
        public async Task GetAll_Single_SeededWater()
        {
            var ingredients = await _carFacadeSUT.GetAsync();
            var ingredient = ingredients.Single(i => i.Id == IngredientSeeds.Water.Id);

            DeepAssert.Equal(Mapper.Map<IngredientListModel>(IngredientSeeds.Water), ingredient);
        }

        [Fact]
        public async Task GetById_SeededWater()
        {
            var ingredient = await _carFacadeSUT.GetAsync(IngredientSeeds.Water.Id);

            DeepAssert.Equal(Mapper.Map<IngredientDetailModel>(IngredientSeeds.Water), ingredient);
        }

        [Fact]
        public async Task GetById_NonExistent()
        {
            var ingredient = await _carFacadeSUT.GetAsync(IngredientSeeds.EmptyIngredient.Id);

            Assert.Null(ingredient);
        }

        [Fact]
        public async Task SeededWater_DeleteById_Deleted()
        {
            await _carFacadeSUT.DeleteAsync(IngredientSeeds.Water.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Ingredients.AnyAsync(i => i.Id == IngredientSeeds.Water.Id));
        }


        [Fact]
        public async Task NewIngredient_InsertOrUpdate_IngredientAdded()
        {
            //Arrange
            var ingredient = new IngredientDetailModel(
                Name: "Water",
                Description: "Mineral water"
            );

            //Act
            ingredient = await _carFacadeSUT.SaveAsync(ingredient);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var ingredientFromDb = await dbxAssert.Ingredients.SingleAsync(i => i.Id == ingredient.Id);
            DeepAssert.Equal(ingredient, Mapper.Map<IngredientDetailModel>(ingredientFromDb));
        }

        [Fact]
        public async Task SeededWater_InsertOrUpdate_IngredientUpdated()
        {
            //Arrange
            var ingredient = new IngredientDetailModel
            (
                Name: IngredientSeeds.Water.Name,
                Description: IngredientSeeds.Water.Description
            )
            {
                Id = IngredientSeeds.Water.Id
            };
            ingredient.Name += "updated";
            ingredient.Description += "updated";

            //Act
            await _carFacadeSUT.SaveAsync(ingredient);

            //Assert
            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var ingredientFromDb = await dbxAssert.Ingredients.SingleAsync(i => i.Id == ingredient.Id);
            DeepAssert.Equal(ingredient, Mapper.Map<IngredientDetailModel>(ingredientFromDb));
        }*/
    }
}
