namespace TravelAgency.DAL.Entities
{
    public record PassengerOfShareRideEntity(
        Guid PassengerId,
        Guid ShareRideId
        )
    {
        public UserEntity? Passenger { get; set; }
        public ShareRideEntity? ShareRide { get; set; }

    }
}