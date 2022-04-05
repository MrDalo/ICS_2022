using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TravelAgency.DAL.Factories
{
    /// <summary>
    /// EF Core CLI migration generation uses this DbContext to create model and migration
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TravelAgencyDbContext>
    {
        public TravelAgencyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TravelAgencyDbContext> builder = new();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = TravelAgency;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");

            return new TravelAgencyDbContext(builder.Options);
        }
    }
}

