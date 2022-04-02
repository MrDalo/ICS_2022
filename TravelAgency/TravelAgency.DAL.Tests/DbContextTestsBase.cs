using System;
using System.Threading.Tasks;
using TravelAgency.Common.Tests;
using TravelAgency.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using Xunit.Abstractions;

namespace TravelAgency.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        // Switch between databases
        //DbContextFactory = new DbContextTestingInMemoryFactory("TravelAgency", true);//GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextTestingLocalDBFactory("TravelAgency", true);

        TravelAgencyDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<TravelAgencyDbContext> DbContextFactory { get; }
    protected TravelAgencyDbContext TravelAgencyDbContextSUT { get; }

    public async Task InitializeAsync()
    {
        await TravelAgencyDbContextSUT.Database.EnsureDeletedAsync();
        await TravelAgencyDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
       // await TravelAgencyDbContextSUT.Database.EnsureDeletedAsync();
        await TravelAgencyDbContextSUT.DisposeAsync();
    }
}