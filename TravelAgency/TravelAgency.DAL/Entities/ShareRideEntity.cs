﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.DAL.Entities
{
    public class ShareRideEntity : IEntity
    {
        public Guid Id { get; set; }
        public string? FromPlace { get; set; }
        public string? ToPlace { get; set; }
        public DateTime LeaveTime { get; set; }
        public DateTime ArriveTime { get; set; }
        public float Cost { get; set; }

        public Guid CarId { get; set; }
        
        [ForeignKey(nameof(CarId))]
        public CarEntity? Car { get; set; }
        
        public Guid DriverId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public UserEntity? Driver { get; set; }

        public ICollection<UserEntity>? CoDrivers { get; set; }
    }
}