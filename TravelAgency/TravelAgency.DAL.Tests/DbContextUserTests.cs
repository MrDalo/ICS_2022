using System;
using System.Linq;
using System.Collections.Generic;
using TravelAgency.Common.Enums;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Common.Tests;
using TravelAgency.Common.Tests.Seeds;
using TravelAgency.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace TravelAgency.DAL.Tests
{
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }
        
        [Fact]
        public async Task AddNew_UserWitNoCar_Persisted()
        {
            //Arrange
            var entity  = UserSeeds.EmptyUserEntity with{
                Id = Guid.Parse(input: "8888A15C-089C-4971-80FE-917A3E2A32E8"),
                Name = "Palko",
                Login= "xsmith00",
                Surname = "ChudobnyBezKary",
                Email= "davidsmith@gmail.com",
                PhoneNumber= "+421789564111",
                Cars = new List<CarEntity>()

            };

            //Act
            TravelAgencyDbContextSUT.Users.Add(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task GetAll_Users_FromDB()
        {
            var users = await TravelAgencyDbContextSUT.Users.ToArrayAsync();

            Assert.True(users.Any());
        }


        [Fact]
        public async Task AddNew_UserWithCar_Persisted()
        {
            //Arrange
            var entity = UserSeeds.EmptyUserEntity with
            {
                Id = Guid.Parse(input: "88C7A15C-089C-4971-80FE-917A3E2A32E8"),
                Name = "Tonko",
                Login = "xsmith00",
                Surname = "Novy",
                Email = "tonkozgurunu@gmail.com",
                PhoneNumber = "+421789564222",
                Cars = new List<CarEntity> {
                    CarSeeds.EmptyCarEntity with
                    {
                        Id = Guid.Parse(input: "2B5BC5E2-175C-4482-B17F-283884706F0A"),
                        LicensePlate= "IL584XG",
                        Manufacturer= "Ford",
                        CarType= CarType.Other,
                        Owner = UserSeeds.EmptyUserEntity
                    }
                }
            };

            //Act
            TravelAgencyDbContextSUT.Users.Add(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users
                .Include(i => i.Cars)
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task GetById_User()   
        {
            //Act
            var entity = await TravelAgencyDbContextSUT.Users
                .SingleAsync(i => i.Id == UserSeeds.UserEntity.Id);

            //Assert
            DeepAssert.Equal(UserSeeds.UserEntity with { Cars = Array.Empty<CarEntity>(), DriverShareRides = Array.Empty<ShareRideEntity>(), PassengerShareRides = Array.Empty<ShareRideEntity>()}, entity);
        }

        [Fact]
        public async Task GetById_IncludingCarsShareRidesPassengerShareRides_User()
        {
            //Act
            var entity = await TravelAgencyDbContextSUT.Users
                .Include(i => i.Cars)
                .Include(i => i.DriverShareRides)
                .Include(i => i.PassengerShareRides)
                .SingleAsync(i => i.Id == UserSeeds.UserEntity.Id);
            //Assert
           
            DeepAssert.Equal(UserSeeds.UserEntity, entity);
        }

        [Fact]
        public async Task Update_User_Persisted()
        {
            //Arrange
            var baseEntity = UserSeeds.UserEntityUpdate;
            var entity =
                baseEntity with
                {
                    Login= "xzmena00",
                    Name= "Pulo",
                    Surname= "Zmeneny",
                    Email= "zmena@gmail.com",
                    PhoneNumber= "0949866579"
                };

            //Act
            TravelAgencyDbContextSUT.Users.Update(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_UserById_Deleted()
        {
            //Arrange
            var baseEntity = UserSeeds.UserCarEntityDelete;

            //Act
           TravelAgencyDbContextSUT.Remove(
                TravelAgencyDbContextSUT.Users.Single(i => i.Id == baseEntity.Id));
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TravelAgencyDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
        }
        
    }
}