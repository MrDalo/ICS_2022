namespace TravelAgency.DAL.Entities
{
    public record PassengerOfShareRideEntity(
        Guid Id,
        Guid PassengerId,
        Guid ShareRideId
        ) : IEntity
    {
        public UserEntity? Passenger { get; set; }
        public ShareRideEntity? ShareRide { get; set; }

    }
}