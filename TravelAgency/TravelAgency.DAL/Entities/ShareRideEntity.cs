using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(CarId))]
        public CarEntity? Car { get; init; }

        [ForeignKey(nameof(DriverId))]
        [InverseProperty("DriverShareRides")] // TODO moze sa po Fluent API vazbach vymazat
        //InverseProperty needs to be add because calss have 2 foreign keys and cant determined which one select in test
        //https://stackoverflow.com/questions/67530126/system-invalidoperationexception-unable-to-determine-the-relationship-represent
        //https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=data-annotations%2Cdata-annotations-simple-key%2Csimple-key#manual-configuration
        public UserEntity? Driver { get; init; }

        public ICollection<UserEntity>? Passengers { get; init; }
    }
}