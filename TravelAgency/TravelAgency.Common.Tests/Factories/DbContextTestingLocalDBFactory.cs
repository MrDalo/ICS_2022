using Microsoft.EntityFrameworkCore;
using TravelAgency.DAL;

namespace TravelAgency.Common.Tests.Factories
{
    public class DbContextTestingLocalDBFactory : IDbContextFactory<TravelAgencyDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextTestingLocalDBFactory(string databaseName, bool seedTestingData)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public TravelAgencyDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TravelAgencyDbContext> builder = new();
            builder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = {_databaseName};MultipleActiveResultSets = True;Integrated Security = True; ");

            // TravelAgencyDbContext seedDemoData set to true in order to run tests
            return new TravelAgencyTestingDbContext(builder.Options, _seedTestingData);
        }
    }
}
