using System.Linq;
using System.Threading.Tasks;
using TravelAgency.BL.Models;
using TravelAgency.BL.Facades;
using TravelAgency.Tests.Common;
using TravelAgency.Tests.Common.Seeds;
using Microsoft.EntityFrameworkCore;
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

            var model = new UserDetailModel
            (
                Login: "xharmy00",
                Name: "Domco",
                Surname: "Hamrynsky",
                Email: "domim@gmail.com",
                PhoneNumber: "+421789562511"
            );


            try
            {
                await _userFacadeSUT.SaveAsync(model);
            }
            catch (DbUpdateException) { }
        }



        [Fact]
        public async Task Update_RemoveCars_FromSeeded_CheckUpdated()
        {

            var detailModel = Mapper.Map<UserDetailModel>(UserSeeds.PassengerTest2);
            detailModel.Cars.Clear();


            await _userFacadeSUT.SaveAsync(detailModel);

            var returnedModel = await _userFacadeSUT.GetAsync(detailModel.Id);
            DeepAssert.Equal(detailModel, returnedModel);
        }

        [Fact]
        public async Task Update_RemoveOneOfCars_FromSeeded_CheckUpdated()
        {

            var detailModel = Mapper.Map<UserDetailModel>(UserSeeds.PassengerTest);
            detailModel.Cars.Remove(detailModel.Cars.First());
            detailModel.Cars.Remove(detailModel.Cars.First());


            await _userFacadeSUT.SaveAsync(detailModel);

            var returnedModel = await _userFacadeSUT.GetAsync(detailModel.Id);
            DeepAssert.Equal(detailModel, returnedModel);
        }


        [Fact]
        public async Task AddUserToShareRideAsAPassenger()
        {

            Assert.True(await _userFacadeSUT.SignUpForShareRideAsPassenger(Mapper.Map<UserDetailModel>(UserSeeds.PassengerTest71), Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideEntityDelete)));

        }


        [Fact]
        public async Task AddUserToShareRideAsAPassengerError()
        {

            Assert.False(await _userFacadeSUT.SignUpForShareRideAsPassenger(Mapper.Map<UserDetailModel>(UserSeeds.Passenger1), Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideEntityDelete)));

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
                    i.PassengerId == passengerModel.PassengerId
                    && i.ShareRideId == passengerModel.ShareRideId);

                if (passengerModelList != null)
                {
                    passengerModel.Id = passengerModelList.Id;
                }
            }



        }

     

    }

}