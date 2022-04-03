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
            var entity  = UserSeeds.UserEntityWithNoCars with{
                Name= "David",
                Login= "xsmith00",
                Surname = "Smith",
                Email= "davidsmith@gmail.com",
                PhoneNumber= "+421789564111"
            };

            //Act
            TravelAgencyDbContextSUT.Users.Add(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntities);
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
                Id = Guid.Parse(input: "{1D5BC5E2-175C-4482-B17F-283884706F0A}"),
                Name = "Tonko",
                Login = "xsmith00",
                Surname = "Tonislav",
                Email = "tonkozgurunu@gmail.com",
                PhoneNumber = "+421789564222",
                Cars = new List<CarEntity> {
                    CarSeeds.CarEntity with
                    {
                        Id = Guid.Parse(input: "{2B5BC5E2-175C-4482-B17F-283884706F0A}"),
                        LicensePlate= "IL584XG",
                        Manufacturer= "Skoda",
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
        public async Task GetById_User()    //TODO v cookbook je v common.test/seeds odlisne data s rovnakym id ako v dal.test/seed a prechadza to nejako zahadne
        {
            //Act
            var entity = await TravelAgencyDbContextSUT.Users
                .SingleAsync(i => i.Id == UserSeeds.UserEntity.Id);

            //Assert
            DeepAssert.Equal(UserSeeds.UserEntity with { Cars = Array.Empty<CarEntity>() }, entity);
        }

        [Fact]
        public async Task GetById_IncludingCars_User()
        {
            //Act
            var entity = await TravelAgencyDbContextSUT.Users
                .Include(i => i.Cars)
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
                    Login= "xsmith00",
                    Name= "Lacko",
                    Surname= "Placko",
                    Email= "vut@gmail.com",
                    PhoneNumber= "0949866579",
                };

            //Act
            TravelAgencyDbContextSUT.Users.Update(entity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

       /* [Fact]
        public async Task Delete_User_Deleted()     //todo problem s odkazom na sharerides
        {
            //Arrange
            var baseEntity = UserSeeds.UserEntity;

            //Act
            TravelAgencyDbContextSUT.Users.Remove(baseEntity);
            await TravelAgencyDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await TravelAgencyDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
        }*/
        

    }
}