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
        public async Task GetDriverPassengerShareRidesTest()
        {
            var share_rides =
                await _shareRideFacadeSUT.GetUserPassengerShareRides(Guid.Parse(input: "87B869FC-8356-4440-9CB7-43E3A996F165"));


            DeepAssert.Equal(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntity1), share_rides.First());
        }



        [Fact]
        public async Task GetDriverShareRides()
        {
            var share_rides =
                await _shareRideFacadeSUT.GetDriverShareRides(Guid.Parse(input: "4c5df605-d676-4c25-98d8-5b795c7b6503"));
            

            DeepAssert.Equal(Mapper.Map<ShareRideListModel>(ShareRideSeeds.ShareRideEntity1), share_rides.First());
        }



        [Fact]
        public async Task ShareRide_FromTrencin()
        {
            var share_rides = await _shareRideFacadeSUT.GetFilteredShareRidesAsync(startLocation: "Trencin");
            var share_ride = ShareRideSeeds.ShareRideToBeUpdated2;

            DeepAssert.Equal(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideToBeUpdated2), share_rides.First());
        }

        [Fact]
        public async Task ShareRides_ToViena()
        {
            var share_rides = await _shareRideFacadeSUT.GetFilteredShareRidesAsync(destinationLocation: "Viena");
            IList<ShareRideDetailModel> ref_share_rides = new List<ShareRideDetailModel>();
            ref_share_rides.Add(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideWithoutPass1));
            ref_share_rides.Add(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideWithoutPass2));
            ref_share_rides.Add(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideWithoutPass3));

            IEnumerable<ShareRideDetailModel> enum_ref_share_rides = ref_share_rides;

            DeepAssert.Equal(enum_ref_share_rides, share_rides);
        }


        [Fact]
        public async Task ShareRides_FromTime()
        {
            var share_rides = await _shareRideFacadeSUT.GetFilteredShareRidesAsync(startTimeFrom: new DateTime(2022, 6, 12, 12, 20, 0));
            IList<ShareRideDetailModel> ref_share_rides = new List<ShareRideDetailModel>();
            ref_share_rides.Add(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideWithoutPass1));
            ref_share_rides.Add(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideWithoutPass2));
            ref_share_rides.Add(Mapper.Map<ShareRideDetailModel>(ShareRideSeeds.ShareRideWithoutPass3));

            IEnumerable<ShareRideDetailModel> enum_ref_share_rides = ref_share_rides;

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
