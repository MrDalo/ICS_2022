using System.ComponentModel.DataAnnotations.Schema;
using TravelAgency.Common.Enums;

namespace TravelAgency.DAL.Entities
{
    public record CarEntity(
        Guid Id,
        string LicensePlate,
        string Manufacturer,
        CarType CarType,
        DateTime RegistrationDate,
        string? ImgUrl,
        int Capacity,
        Guid OwnerId) : IEntity
    {

        [ForeignKey(nameof(OwnerId))]
        public UserEntity? Owner { get; init; }
    }
}