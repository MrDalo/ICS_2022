using System;
using System.Collections.Generic;
using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record FilteredRideWindowMessage(IEnumerable<ShareRideDetailModel> filteredShareRide, string? FromPlace, string? ToPlace, DateTime Time1, DateTime Time2, Guid UserId) : IMessage
    {
    }
}
