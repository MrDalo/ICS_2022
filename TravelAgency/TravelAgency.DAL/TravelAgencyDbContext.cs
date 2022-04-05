using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.Seeds;

namespace TravelAgency.DAL
{
    public class TravelAgencyDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        /**
         * @brief Construction of DbContext
         */
        public TravelAgencyDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
            : base(contextOptions)
        {
            _seedDemoData = seedDemoData;
        }

        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<ShareRideEntity> ShareRides => Set<ShareRideEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarEntity>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Cars)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ShareRideEntity>()
                .HasOne(s => s.Car)
                .WithMany() 
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShareRideEntity>()
                .HasOne(s => s.Driver)
                .WithMany(u => u.DriverShareRides)
                .HasForeignKey(s => s.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<ShareRideEntity>()
                .HasMany(s => s.Passengers)
                .WithMany(u => u.PassengerShareRides)
                .UsingEntity(j => j.ToTable("PassengerOfShareRide")); // Malo by to vytvorit novu tabulku s nazvom PassengerOfShareRide - N k N vztah je tvorba novej tabulky
                                              //src: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-composite-key%2Csimple-key

            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                ShareRideSeeds.Seed(modelBuilder);
            }
        }
    }
}
