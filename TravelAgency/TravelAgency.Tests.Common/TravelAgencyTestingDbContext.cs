using TravelAgency.DAL;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Tests.Common.Seeds;

namespace TravelAgency.Tests.Common
{
    public class TravelAgencyTestingDbContext : TravelAgencyDbContext
    {
        private readonly bool _seedTestingData;

        public TravelAgencyTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
            : base(contextOptions, seedDemoData: false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_seedTestingData)
            {
                UserSeeds.Seed(modelBuilder);
                CarSeeds.Seed(modelBuilder);
                ShareRideSeeds.Seed(modelBuilder);
                PassengerOfShareRideSeeds.Seed(modelBuilder);
            }
        }
    }
}