using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record UpdateMessage<T> : Message<T>
        where T : IModel
    {
    }
}
