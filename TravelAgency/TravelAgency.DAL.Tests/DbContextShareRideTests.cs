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



    }

}