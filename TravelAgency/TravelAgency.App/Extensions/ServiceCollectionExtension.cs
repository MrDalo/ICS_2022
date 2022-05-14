using System;
using TravelAgency.App.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.App.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();

            services.AddSingleton<Func<TService>>(x => x.GetRequiredService<TService>);

            services.AddSingleton<IFactory<TService>, Factory<TService>>();
        }

    }
}
