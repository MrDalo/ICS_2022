using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TravelAgency.BL.Tests;
using TravelAgency.BL.Facades;
using TravelAgency.Common.Tests;
using TravelAgency.Common.Tests.Seeds;

using Microsoft.EntityFrameworkCore;
using TravelAgency.BL.Models;
using Xunit.Abstractions;
using Xunit;

namespace TravelAgency.BL.Tests
{
    public sealed class ShareRideFacadeTests : CRUDFacadeTestsBase
    {
        private readonly CarFacade _shareRideFacadeSUT;

        public ShareRideFacadeTests(ITestOutputHelper output) : base(output)
        {
            _shareRideFacadeSUT = new CarFacade(UnitOfWorkFactory, Mapper);
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
                Cost: 50
            );

            var returnedModel = await _shareRideFacadeSUT.SaveAsync(model);
            FixIds(model,returnedModel);
            DeepAssert.Equal(model, returnedModel);
        }

        private static void FixIds(UserDetailModel expectedModel, UserDetailModel returnedModel)
        {
            returnedModel.Id = expectedModel.Id;

            foreach (var userModel in returnedModel.Passengers)
            {
                var userListModel = expectedModel.Passengers.FirstOrDefault(i =>
                    i.Login == userModel.Login
                    && i.Name == userModel.Name
                    && i.Surname == userModel.Surname
                    && i.Email == userModel.Email
                    && i.PhoneNumber == userModel.PhoneNumber);

                if (userListModel != null)
                {
                    userModel.Id = userListModel.Id;
                }
            }
        }
    }
}
