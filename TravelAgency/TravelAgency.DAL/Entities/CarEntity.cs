using TravelAgency.DAL.Enums;

namespace TravelAgency.DAL.Entities
{
    public class CarEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public CarType CarType { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ImgUrl { get; set; }
        public int Capacity { get; set; }

        public UserEntity Owner { get; set; }

        public ICollection<ShareRideEntity> ShareRides { get; set; }
    }
}