
namespace TravelAgency.DAL.Entities
{
    public record ShareRideEntity(
        Guid Id,
        string FromPlace,
        string ToPlace,
        DateTime LeaveTime,
        DateTime ArriveTime,
        decimal Cost,
        Guid CarId,
        Guid DriverId) : IEntity
    {

        public CarEntity? Car { get; init; }

        public UserEntity? Driver { get; init; }

        public ICollection<UserEntity> Passengers { get; init; } = new List<UserEntity>();
    }
}