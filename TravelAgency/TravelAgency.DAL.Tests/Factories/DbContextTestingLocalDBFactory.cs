﻿using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Tests.Factories
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
            return new TravelAgencyDbContext(builder.Options, _seedTestingData);
        }
    }
}
