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
        public DbSet<PassengerOfShareRideEntity> PassengerOfShareRide => Set<PassengerOfShareRideEntity>();


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
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ShareRideEntity>()
                .HasOne(s => s.Driver)
                .WithMany(u => u.DriverShareRides)
                .HasForeignKey(s => s.DriverId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PassengerOfShareRideEntity>()
                .HasKey(x => new {x.PassengerId, x.ShareRideId});

            modelBuilder.Entity<PassengerOfShareRideEntity>()
                .HasOne(p => p.Passenger)
                .WithMany(u => u.PassengerShareRides)
                .HasForeignKey(p => p.PassengerId);

            modelBuilder.Entity<PassengerOfShareRideEntity>()
                .HasOne(p => p.ShareRide)
                .WithMany(u => u.Passengers)
                .HasForeignKey(p => p.ShareRideId);

            
            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                ShareRideSeeds.Seed(modelBuilder);
                PassengerOfShareRideSeeds.Seed(modelBuilder);
            }
        }
    }
}
