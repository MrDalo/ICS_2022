using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record NewMessage<T> : Message<T>
        where T : IModel
    {
    }
}
