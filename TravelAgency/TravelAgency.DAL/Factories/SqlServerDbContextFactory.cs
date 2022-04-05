﻿using Microsoft.EntityFrameworkCore;

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

            //optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            //optionsBuilder.EnableSensitiveDataLogging();

            return new TravelAgencyDbContext(optionsBuilder.Options, _seedDemoData);
        }
    }
}