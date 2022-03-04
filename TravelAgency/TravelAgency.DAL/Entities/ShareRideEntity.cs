using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.DAL.Entities
{
    public record ShareRideEntity(
        Guid Id,
        string FromPlace,
        string ToPlace,
        DateTime LeaveTime,
        DateTime ArriveTime,
        float Cost,
        Guid CarId,
        Guid DriverId) : IEntity
    {

        [ForeignKey(nameof(CarId))]
        public CarEntity? Car { get; init; }

        [ForeignKey(nameof(DriverId))]
        public UserEntity? Driver { get; init; }

        public ICollection<UserEntity>? CoDrivers { get; init; }
    }
}