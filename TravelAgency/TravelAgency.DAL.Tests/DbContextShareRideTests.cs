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
    public class DbContextShareRidesTests : DbContextTestsBase
    {
        public DbContextShareRidesTests(ITestOutputHelper output) : base(output)
        {
        }


        [Fact]
        public async Task AddNew_ShareRide_Persisted()
        {
            //Arrange
            ShareRideEntity entity = new(
                Guid.Parse("111145D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
                FromPlace: "Nitra",
                ToPlace: "Trencin",
                LeaveTime: new DateTime(2022, 4, 12, 12, 30, 0),
                ArriveTime: new DateTime(2022, 4, 12, 14, 30, 0),
                Cost: 5,
                CarId: CarSeeds.CarEntity1.Id,
                DriverId: UserSeeds.UserEntity.Id)
            {
                Car = null,
                Driver = null
            };

            //Act
            TravelAgencyDbContextSUT.ShareRides.Add(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.ShareRides.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
        }

        [Fact]
        public async Task GetAll_ShareRide_FromDB()
        {
            var ride = await TravelAgencyDbContextSUT.ShareRides.ToArrayAsync();

            Assert.True(ride.Any());
        }

        [Fact]
        public async Task Update_Shareride_Persisted()
        {
            //Arrange
            var baseEntity = ShareRideSeeds.ShareRideEntityUpdate;
            var entity =
                baseEntity with
                {
                    Cost = Convert.ToDecimal("2,5"),
                    ArriveTime= new DateTime(2022, 4, 12, 15, 30, 0)
                };

            //Act
            TravelAgencyDbContextSUT.ShareRides.Update(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.ShareRides.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task DeleteById_ShareRide_Deleted()
        {
            //Arrange
            var baseEntity = ShareRideSeeds.ShareRideEntityDelete;

            //Act
            TravelAgencyDbContextSUT.Remove(
                TravelAgencyDbContextSUT.ShareRides.Single(i => i.Id == baseEntity.Id));
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TravelAgencyDbContextSUT.ShareRides.AnyAsync(i => i.Id == baseEntity.Id));
        }

    }

}