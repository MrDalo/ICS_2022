using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.Common.Tests;
using TravelAgency.Common.Tests.Factories;
using TravelAgency.DAL;
using TravelAgency.DAL.Factories;
using TravelAgency.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TravelAgency.BL.Tests;

public class CRUDFacadeTestsBase : IAsyncLifetime
{
    protected CRUDFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        DbContextFactory = new DbContextTestingLocalDBFactory(GetType().FullName!, seedTestingData: true);
        

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(new[]
            {
                    typeof(BusinessLogic),
                });
            cfg.AddCollectionMappers();

            using var dbContext = DbContextFactory.CreateDbContext();
            cfg.UseEntityFrameworkCoreModel<TravelAgencyDbContext>(dbContext.Model);
        }
        );
        Mapper = new Mapper(configuration);
        Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    protected IDbContextFactory<TravelAgencyDbContext> DbContextFactory { get; }

    protected Mapper Mapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}