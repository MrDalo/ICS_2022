using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Common.Enums;
using TravelAgency.Common.Tests;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.Seeds;
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
            CarEntity entity = new(
                //Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
                Id: Guid.Parse(input: "C9D2B626-05F3-4878-B174-24AA7C8E1773"),
                LicensePlate: "IL012DD",
                Manufacturer: "Skoda",
                CarType.Sedan,
                RegistrationDate: DateTime.Today, 
                ImgUrl: "https://fiat-zilina.sk/vozidlo/fiat-doblo/",
                Capacity: 5,
                OwnerId: UserSeeds.Driver1.Id
            );

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
        public async Task GetAll_Fiat_Cars_FromDB()
        {
           
            var fiat = await TravelAgencyDbContextSUT.Cars.ToArrayAsync();

            Assert.Contains(CarSeeds.FiatMultipla, fiat);
        }

        [Fact]
        public async Task GetById_Cars_FiatRetrieved()
        {

            var fiat = await TravelAgencyDbContextSUT.Cars.SingleAsync(i => i.Id == CarSeeds.FiatMultipla.Id);

            Assert.Equal(CarSeeds.FiatMultipla, fiat);
        }

    }
}