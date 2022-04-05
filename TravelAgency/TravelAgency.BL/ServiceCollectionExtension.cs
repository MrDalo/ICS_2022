using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TravelAgency.BL.Facades;
using TravelAgency.DAL;
using TravelAgency.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.BL;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddSingleton<UserFacade>();
        services.AddSingleton<ShareRideFacade>();
        services.AddSingleton<CarFacade>();

        services.AddAutoMapper((serviceProvider, cfg) =>
        {
            cfg.AddCollectionMappers();

            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<TravelAgencyDbContext>>();
            using var dbContext = dbContextFactory.CreateDbContext();
            cfg.UseEntityFrameworkCoreModel<TravelAgencyDbContext>(dbContext.Model);
        }, typeof(BusinessLogic).Assembly);
        return services;
    }
}