using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record DeleteMessage<T> : Message<T>
        where T : IModel
    {
    }
}
