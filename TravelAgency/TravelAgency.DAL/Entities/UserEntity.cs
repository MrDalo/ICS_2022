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

        public ICollection<ShareRideEntity> DriverShareRides { get; init; } = new List<ShareRideEntity>();

        public ICollection<ShareRideEntity> PassengerShareRides { get; init; } = new List<ShareRideEntity>();

        public ICollection<CarEntity> Cars { get; init; } = new List<CarEntity>();

    }
}