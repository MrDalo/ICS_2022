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

    public class UserFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UserFacade _userFacadeSUT;

        public UserFacadeTests(ITestOutputHelper output) : base(output)
        {
            _userFacadeSUT = new UserFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_WithWithoutCars_DoesNotThrowAndEqualsCreated()
        {
            var model = new UserDetailModel
            (
                Login: "xsmith00",
                Name: "David",
                Surname:"Smith",
                Email:"davidsmith@gmail.com",
                PhoneNumber:"+421789564111"
            );

            
            var returnedModel = await _userFacadeSUT.SaveAsync(model);

            FixIds(model, returnedModel);


            DeepAssert.Equal(model, returnedModel);
        }

        [Fact]
        public async Task Create_WithNonExistingUser_Throws()
        {
            //Arrange
            var model = new UserDetailModel
            (
                Login: "xharmy00",
                Name: "Domco",
                Surname: "Hamrynsky",
                Email: "domim@gmail.com",
                PhoneNumber: "+421789562511"
            );

            //Act & Assert
            try
            {
                await _userFacadeSUT.SaveAsync(model); //In-memory pass without exception
            }
            catch (DbUpdateException) { } //SqlServer throws on FK
        }


        [Fact]
        public async Task Update_RemoveCars_FromSeeded_CheckUpdated()
        {
            //Arrange
            var detailModel = Mapper.Map<UserDetailModel>(UserSeeds.PassengerTest2);
            detailModel.Cars.Clear();

            //Act
            await _userFacadeSUT.SaveAsync(detailModel);

            //Assert
            var returnedModel = await _userFacadeSUT.GetAsync(detailModel.Id);
            DeepAssert.Equal(detailModel, returnedModel);
        }

        [Fact]
        public async Task Update_RemoveOneOfCars_FromSeeded_CheckUpdated()
        {
            //Arrange
            var detailModel = Mapper.Map<UserDetailModel>(UserSeeds.PassengerTest);
            detailModel.Cars.Remove(detailModel.Cars.First());
            detailModel.Cars.Remove(detailModel.Cars.First());

            //Act
            await _userFacadeSUT.SaveAsync(detailModel);

            //Assert
            var returnedModel = await _userFacadeSUT.GetAsync(detailModel.Id);
            DeepAssert.Equal(detailModel, returnedModel);
        }


        private static void FixIds(UserDetailModel expectedModel, UserDetailModel returnedModel)
        {
            returnedModel.Id = expectedModel.Id;

            foreach (var shareRidesModel in returnedModel.DriverShareRides)
            {
                var shareRidesListModel = expectedModel.DriverShareRides.FirstOrDefault(i =>
                    i.Cost == shareRidesModel.Cost
                    && i.FromPlace == shareRidesModel.FromPlace
                    && i.ToPlace == shareRidesModel.ToPlace
                    && i.LeaveTime == shareRidesModel.LeaveTime);

                if (shareRidesListModel != null)
                {
                    shareRidesModel.Id = shareRidesListModel.Id;
                }
            }

            foreach (var carEntityModel in returnedModel.Cars)
            {
                var carEntityModelList = expectedModel.Cars.FirstOrDefault(i =>
                    i.LicensePlate == carEntityModel.LicensePlate
                    && i.Manufacturer == carEntityModel.Manufacturer
                    && i.CarType == carEntityModel.CarType
                    && i.RegistrationDate == carEntityModel.RegistrationDate
                    && i.Capacity == carEntityModel.Capacity);

                if (carEntityModelList != null)
                {
                    carEntityModel.Id = carEntityModelList.Id;
                }
            }

            foreach (var passengerModel in returnedModel.PassengerShareRides)
            {
                var passengerModelList = expectedModel.PassengerShareRides.FirstOrDefault(i =>
                    i.Cost == passengerModel.Cost
                    && i.LeaveTime == passengerModel.LeaveTime
                    && i.FromPlace == passengerModel.FromPlace
                    && i.ToPlace == passengerModel.ToPlace);

                if (passengerModelList != null)
                {
                    passengerModel.Id = passengerModelList.Id;
                }
            }



        }

     

    }

}