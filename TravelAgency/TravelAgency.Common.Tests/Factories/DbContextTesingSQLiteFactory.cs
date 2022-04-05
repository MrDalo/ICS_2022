using Microsoft.EntityFrameworkCore;
using TravelAgency.DAL;

namespace TravelAgency.Common.Tests.Factories
{
    public class DbContextTestingSQLiteFactory : IDbContextFactory<TravelAgencyDbContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextTestingSQLiteFactory(string databaseName, bool seedTestingData = false)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public TravelAgencyDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<TravelAgencyDbContext> builder = new();
            builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");


            return new TravelAgencyTestingDbContext(builder.Options, _seedTestingData);
        }

    }
}
