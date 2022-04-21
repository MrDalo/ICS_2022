using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record AddedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
