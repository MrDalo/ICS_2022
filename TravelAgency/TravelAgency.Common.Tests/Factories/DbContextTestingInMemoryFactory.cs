using Microsoft.EntityFrameworkCore;
using TravelAgency.DAL;

namespace TravelAgency.Common.Tests.Factories
{
    public class DbContextTestingInMemoryFactory : IDbContextFactory<TravelAgencyDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextTestingInMemoryFactory(string databaseName, bool seedTestingData)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public TravelAgencyDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TravelAgencyDbContext> contextOptionsBuilder = new();
            contextOptionsBuilder.UseInMemoryDatabase(_databaseName);

            // TravelAgencyDbContext seedDemoData set to true in order to run tests
            return new TravelAgencyDbContext(contextOptionsBuilder.Options, _seedTestingData);
        }
    }
}
