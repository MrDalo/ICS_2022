using System;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.BL.Models;
using TravelAgency.BL.Facades;
using TravelAgency.Tests.Common;
using TravelAgency.Tests.Common.Seeds;
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
            var cars = await _carFacadeSUT.GetAll();
            var car = cars.Single(i => i.Id == CarSeeds.CarEntity1.Id);

            DeepAssert.Equal(Mapper.Map<CarListModel>(CarSeeds.CarEntity1), car);
        }

        [Fact]
        public async Task GetById_SeededCar()
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

        [Fact]
        public async Task SeededCar_InsertOrUpdate_ParametersUpdated()
        {
 
            var carUpdate = new CarDetailModel
            (
                LicensePlate: CarSeeds.CarToBeUpdated.LicensePlate,
                Manufacturer:CarSeeds.CarToBeUpdated.Manufacturer,
                CarType:CarSeeds.CarToBeUpdated.CarType,
                RegistrationDate: CarSeeds.CarToBeUpdated.RegistrationDate,
                Capacity:CarSeeds.CarToBeUpdated.Capacity,
                OwnerId:CarSeeds.CarToBeUpdated.OwnerId
            )
            {
                Id = CarSeeds.CarToBeUpdated.Id
            };

            carUpdate.Manufacturer = "Hyundai";
            carUpdate.LicensePlate = "IL584UG";
            carUpdate.RegistrationDate = DateTime.Today;

            await _carFacadeSUT.SaveAsync(carUpdate);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var carFromDb = await dbxAssert.Cars.SingleAsync(i => i.Id == carUpdate.Id);
            Assert.Equal(carUpdate, Mapper.Map<CarDetailModel>(carFromDb));
        }
    }
}
