using System;

namespace TravelAgency.App.Messages
{
    public record LoadUserProfile(Guid Id) : IMessage
    {
    }
}
