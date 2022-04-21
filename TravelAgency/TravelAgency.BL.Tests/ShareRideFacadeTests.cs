using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.BL.Facades;
using TravelAgency.Tests.Common;
using TravelAgency.Tests.Common.Seeds;
using Microsoft.EntityFrameworkCore;
using TravelAgency.BL.Models;
using Xunit.Abstractions;
using Xunit;

namespace TravelAgency.BL.Tests
{
    public sealed class ShareRideFacadeTests : CRUDFacadeTestsBase
    {
        private readonly ShareRideFacade _shareRideFacadeSUT;

        public ShareRideFacadeTests(ITestOutputHelper output) : base(output)
        {
            _shareRideFacadeSUT = new ShareRideFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task Create_WithNonExistingItem_DoesNotThrow()
        {
            var model = new ShareRideDetailModel
            (
                FromPlace: "Moscow",
                ToPlace: "Trencin",
                LeaveTime: new DateTime (2022,10,12,12,50,30),
                ArriveTime: new DateTime(2022, 10, 14, 12, 50, 30),
                Cost: 50,
                CarId: Guid.Parse(input: "1206bea4-b4b2-41a1-bf70-4dc610283298"),
                DriverId: Guid.Parse(input: "4c5df605-d676-4c25-98d8-5b795c7b6503")

            );

            var returnedModel = await _shareRideFacadeSUT.SaveAsync(model);
            FixIds(model,returnedModel);
            DeepAssert.Equal(model, returnedModel);
        }

        [Fact]
        public async Task ShareRide_FromTrencin()
        {
            var share_rides = await _shareRideFacadeSUT.GetFilteredShareRidesAsync(startLocation: "Trencin");
            var share_ride = ShareRideSeeds.ShareRideToBeUpdated2;

            DeepAssert.Equal(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideToBeUpdated2), share_rides.First());
        }

        [Fact]
        public async Task ShareRides_ToBrno()
        {
            var share_rides = await _shareRideFacadeSUT.GetFilteredShareRidesAsync(destinationLocation: "Brno");
            IList<ShareRideListModel> ref_share_rides = new List<ShareRideListModel>();
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideToBeUpdated1));
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntity1));
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntityDelete));
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntityUpdate));

            IEnumerable<ShareRideListModel> enum_ref_share_rides = ref_share_rides;

            DeepAssert.Equal(enum_ref_share_rides, share_rides);
        }


        [Fact]
        public async Task ShareRides_FromTime()
        {
            var share_rides = await _shareRideFacadeSUT.GetFilteredShareRidesAsync(startTime: new DateTime(2022, 4, 12, 12, 20, 0));
            IList<ShareRideListModel> ref_share_rides = new List<ShareRideListModel>();
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntity1));
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntityDelete));
            ref_share_rides.Add(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntityUpdate));

            IEnumerable<ShareRideListModel> enum_ref_share_rides = ref_share_rides;

            DeepAssert.Equal(enum_ref_share_rides, share_rides);
        }

        [Fact]
        public async Task ShareRides_IsCapacityFull()
        {
            var share_rides =
                await _shareRideFacadeSUT.IsShareRideFull(
                    Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideEntityUpdate));

            DeepAssert.Equal( share_rides, 1);
        }

        [Fact]
        public async Task ShareRides_IsCapacityNotFull()
        {
            var share_rides =
                await _shareRideFacadeSUT.IsShareRideFull(
                    Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideEntityDelete));

            DeepAssert.Equal(share_rides, 0);
        }


        [Fact]
        public async Task GetAll_Single_SeededShareRideByID()
        {
            var share_rides = await _shareRideFacadeSUT.GetAll();
            var share_ride = share_rides.Single(i => i.Id == ShareRideSeeds.ShareRideToBeUpdated1.Id);

            DeepAssert.Equal(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideToBeUpdated1), share_ride);
        }

        [Fact]
        public async Task GetById_SeededShareRide()
        {
            var share_ride = await _shareRideFacadeSUT.GetAsync(ShareRideSeeds.ShareRideToBeUpdated2.Id);

            DeepAssert.Equal(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideToBeUpdated2), share_ride);
        }

        [Fact]
        public async Task CreateNewShareRide()
        {
            var model = new ShareRideDetailModel
            (
                FromPlace: "New York",
                ToPlace: "Canada",
                LeaveTime: new DateTime(2022, 10, 12, 12, 50, 30),
                ArriveTime: new DateTime(2022, 10, 14, 12, 50, 30),
                Cost: 50,
                CarId: Guid.Parse(input: "1206bea4-b4b2-41a1-bf70-4dc610283298"),
                DriverId: Guid.Parse(input: "4c5df605-d676-4c25-98d8-5b795c7b6503")

            );

            var returnedModel = await _shareRideFacadeSUT.SaveAsync(model);
            FixIds(model, returnedModel);
            DeepAssert.Equal(model, returnedModel);
        }



        [Fact]
        public async Task SeedsCars_SeedersUpdated()
        {
            var currently_update = new ShareRideDetailModel(
                FromPlace: ShareRideSeeds.ShareRideToBeUpdated1.FromPlace,
                ToPlace: ShareRideSeeds.ShareRideToBeUpdated1.ToPlace,
                Cost: ShareRideSeeds.ShareRideToBeUpdated1.Cost,
                LeaveTime: ShareRideSeeds.ShareRideToBeUpdated1.LeaveTime,
                ArriveTime: ShareRideSeeds.ShareRideToBeUpdated1.ArriveTime,
                CarId: ShareRideSeeds.ShareRideToBeUpdated1.CarId,
                DriverId: ShareRideSeeds.ShareRideToBeUpdated1.DriverId)
            {
                Id = ShareRideSeeds.ShareRideToBeUpdated1.Id
            };

            currently_update.LeaveTime = new DateTime(2022, 2, 13, 5, 43, 0);

            await _shareRideFacadeSUT.SaveAsync(currently_update);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            var ShareRideFromDb = await dbxAssert.ShareRides.SingleAsync(i => i.Id == currently_update.Id);
            DeepAssert.Equal(currently_update, Mapper.Map<ShareRideDetailModel>(ShareRideFromDb));

        }



        private static void FixIds(ShareRideDetailModel expectedModel, ShareRideDetailModel returnedModel)
        {
            returnedModel.Id = expectedModel.Id;

            foreach (var userModel in returnedModel.Passengers)
            {
                var userListModel = expectedModel.Passengers.FirstOrDefault(i =>
                    i.PassengerId == userModel.PassengerId 
                    && i.ShareRideId == userModel.ShareRideId 
                );

                if (userListModel != null)
                {
                    userModel.Id = userListModel.Id;
                }
            }
        }
    }
}
