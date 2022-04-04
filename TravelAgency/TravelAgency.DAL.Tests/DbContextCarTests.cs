using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Common.Enums;
using TravelAgency.Common.Tests;
using TravelAgency.Common.Tests.Seeds;
using TravelAgency.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace TravelAgency.DAL.Tests
{
    public class DbContextCarTests : DbContextTestsBase
    {
        public DbContextCarTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNew_Car()
        {
            //Arrange
            var entity = CarSeeds.EmptyCarEntity with{

                Id= Guid.Parse(input: "18501907-3B8B-447C-9F20-9248C1A2C939"),
                LicensePlate= "TN123DD",
                Manufacturer= "BMW",
                CarType= CarType.SportsCar,
                ImgUrl= null,
                RegistrationDate= DateTime.Parse("2021-10-10"),
                Capacity= 5,
                OwnerId= UserSeeds.UserEntity1.Id,
            
                Owner = null
            };

            //Act
            TravelAgencyDbContextSUT.Cars.Add(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualEntities);
        }
        


        [Fact]
        public async Task GetAll_Cars_FromDB()
        {
            var cars = await TravelAgencyDbContextSUT.Cars.ToArrayAsync();

            Assert.True(cars.Any());
        }


        [Fact]
        public async Task GetContains_Car_ForUser()
        {
            var cars = await TravelAgencyDbContextSUT.Cars
                .Where(i => i.OwnerId == CarSeeds.CarEntityUserContains.OwnerId)
                .ToArrayAsync();

            Assert.Contains(CarSeeds.CarEntityUserContains with { Owner = null }, cars);
        }

        [Fact]
        public async Task Delete_CarById_Deleted()
        {
            //Arrange
            var baseEntity = CarSeeds.CarEntityDelete;

            //Act
            TravelAgencyDbContextSUT.Remove(
                TravelAgencyDbContextSUT.Cars.Single(i => i.Id == baseEntity.Id));
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TravelAgencyDbContextSUT.Cars.AnyAsync(i => i.Id == baseEntity.Id));
        }

        [Fact]
        public async Task GetAll_Car_ForUser()
        {
            //Act
            var cars = await TravelAgencyDbContextSUT.Cars
                .Where(i => i.OwnerId == UserSeeds.UserEntity.Id)
                .ToArrayAsync();

            //Assert
            Assert.Contains(CarSeeds.CarEntity1 with { Owner = null}, cars);
            Assert.Contains(CarSeeds.CarEntity2 with { Owner = null}, cars);
        }

    }
}