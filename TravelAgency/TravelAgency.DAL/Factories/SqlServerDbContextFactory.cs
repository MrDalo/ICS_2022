using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<TravelAgencyDbContext>
    {
        private readonly string _connectionString;
        private readonly bool _seedDemoData;

        public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
        {
            _connectionString = connectionString;
            _seedDemoData = seedDemoData;
        }

        public TravelAgencyDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TravelAgencyDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            
            return new TravelAgencyDbContext(optionsBuilder.Options, _seedDemoData);
        }
    }
}