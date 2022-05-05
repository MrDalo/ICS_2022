using System;

namespace TravelAgency.App.Messages
{
    public record OpenSearchRideMessage(Guid UserId) : IMessage
    {
    }
}
