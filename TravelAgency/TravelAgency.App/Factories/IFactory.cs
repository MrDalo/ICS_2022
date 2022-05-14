

namespace TravelAgency.App.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
