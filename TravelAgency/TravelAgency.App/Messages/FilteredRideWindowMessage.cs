using System.Collections.Generic;
using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record FilteredRideWindowMessage(IEnumerable<ShareRideDetailModel> filteredShareRide, string? FromPlace, string? ToPlace) : IMessage
    {
    }
}
