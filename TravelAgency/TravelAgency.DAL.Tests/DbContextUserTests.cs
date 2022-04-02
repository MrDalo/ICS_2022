using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Common.Tests;
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
        public async Task AddNew_User_Persisted()
        {
            //Arrange
            UserEntity entity = new(
                Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
                //Id: Guid.Parse(input: "dc5df605-d676-4c25-98d8-5b795c7b6503"),
                Name: "David",
                Login: "xsmith00",
                Surname: "Smith",
                Email: "davidsmith@gmail.com",
                PhoneNumber: "+421789564111",
                PhotoUrl: null
            );

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
    }
}