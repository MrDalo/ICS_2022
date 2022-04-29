using System;

namespace TravelAgency.App.Messages
{
    public record LoadUserCarsMessage(Guid Id) : IMessage
    {
    }
}
