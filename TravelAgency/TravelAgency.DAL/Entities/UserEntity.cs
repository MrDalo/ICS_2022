namespace TravelAgency.DAL.Entities
{
    public record UserEntity(
        Guid Id,
        string Login,
        string Name,
        string Surname,
        string Email,
        string PhoneNumber,
        string? PhotoUrl) : IEntity
    {

        public ICollection<ShareRideEntity>? DriverShareRides { get; init; }

        public ICollection<ShareRideEntity>? PassengerShareRides { get; init; }

        public ICollection<CarEntity>? Cars { get; init; }
    }
}