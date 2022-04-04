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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                Capacity:5,
                OwnerId: Guid.Parse(input: "5c5df605-d676-4c25-98d8-5b795c7b6503")
            );

            var returnedModel = await _carFacadeSUT.SaveAsync(model);
            FixIds(model, returnedModel);
            Assert.Equal(model, returnedModel);
        }

        private static void FixIds(CarDetailModel expectedModel, CarDetailModel returnedModel)
        {
            returnedModel.Id = expectedModel.Id;
        }
        
        [Fact]
        public async Task GetAll_Single_SeededCarByID()
        {
            var cars = await _carFacadeSUT.GetAsync();
            var car = cars.Single(i => i.Id == CarSeeds.CarEntity1.Id);

            DeepAssert.Equal(Mapper.Map<CarListModel>(CarSeeds.CarEntity1), car);
        }

        [Fact]
        public async Task GetById_SeededWater()
        {
            var car = await _carFacadeSUT.GetAsync(CarSeeds.CarToBeSearched.Id);

            DeepAssert.Equal(Mapper.Map<CarDetailModel>(CarSeeds.CarToBeSearched), car);
        }

        [Fact]
        public async Task GetById_NonExistent()
        {
            var car = await _carFacadeSUT.GetAsync(CarSeeds.EmptyCarEntity.Id);

            Assert.Null(car);
        }

        
        [Fact]
        public async Task SeededCar_DeleteById_Deleted()
        {

            await _carFacadeSUT.DeleteAsync(CarSeeds.CarToBeDeleted.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Cars.AnyAsync(i => i.Id == CarSeeds.CarToBeDeleted.Id));
        }

        /*
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
