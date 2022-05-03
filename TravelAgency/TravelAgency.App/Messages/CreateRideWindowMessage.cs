using System;

namespace TravelAgency.App.Messages
{
    public record CreateRideWindowMessage(System.Guid userID) : IMessage
    {
    }
}
