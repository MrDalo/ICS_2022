﻿namespace TravelAgency.DAL.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }

        public ICollection<ShareRideEntity>? DriverShareRides { get; set; }

        public ICollection<ShareRideEntity>? CoDriverShareRides { get; set; }

        public ICollection<CarEntity>? Cars { get; set; }
    }
}