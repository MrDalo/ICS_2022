using TravelAgency.BL.Models;

namespace TravelAgency.App.Messages
{
    public record SelectedMessage<T> : Message<T>
        where T : IModel
    {
    }
}
