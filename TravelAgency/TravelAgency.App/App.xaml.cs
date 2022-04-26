//using TravelAgency.App.Extensions;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.App.ViewModels;
using TravelAgency.App.Views;
using TravelAgency.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using TravelAgency.App.Factories;
using TravelAgency.App.Settings;
using TravelAgency.App.Extensions;
using TravelAgency.BL;
using TravelAgency.BL.Facades;
using TravelAgency.DAL.Factories;
using TravelAgency.DAL.UnitOfWork;
using Microsoft.Extensions.Options;

namespace TravelAgency.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"appsettings.json", false, false);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddBLServices();

            services.Configure<DALSettings>(configuration.GetSection("TravelAgency:DAL"));

            services.AddSingleton<IDbContextFactory<TravelAgencyDbContext>>(provider =>
            {
                var dalSettings = provider.GetRequiredService<IOptions<DALSettings>>().Value;
                return new SqlServerDbContextFactory(dalSettings.ConnectionString!, dalSettings.SkipMigrationAndSeedDemoData);
            });

            services.AddSingleton<MainWindow>();

            services.AddSingleton<IMessageDialogService, MessageDialogService>();
            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SelectOptionViewModel>();
            services.AddSingleton<IProfileWindowViewModel, ProfileWindowViewModel>();
            services.AddSingleton<IProfileInfoViewModel, ProfileInfoViewModel>();
            services.AddSingleton<IUserCarsViewModel, UserCarsViewModel>();
            services.AddSingleton<IUserRidesViewModel, UserRidesViewModel>();
            services.AddSingleton<IUserListViewModel, UserListViewModel>();
            services.AddSingleton<IShareRideListViewModel, ShareRideListViewModel>();
            services.AddSingleton<IPassengerOfShareRideListViewModel, PassengerOfShareRideListViewModel>();
            services.AddSingleton<ICarListViewModel, CarListViewModel>();

            services.AddFactory<IUserDetailViewModel, UserDetailViewModel>();
            services.AddFactory<ISelectOptionViewModel, SelectOptionViewModel>();
            services.AddFactory<IShareRideDetailViewModel, ShareRideDetailViewModel>();
            services.AddFactory<IPassengerOfShareRideListViewModel, PassengerOfShareRideListViewModel>();
            services.AddFactory<ICarDetailViewModel, CarDetailViewModel>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var dbContextFactory = _host.Services.GetRequiredService<IDbContextFactory<TravelAgencyDbContext>>();

            var dalSettings = _host.Services.GetRequiredService<IOptions<DALSettings>>().Value;

            await using (var dbx = await dbContextFactory.CreateDbContextAsync())
            {
                if (dalSettings.SkipMigrationAndSeedDemoData)
                {
                    await dbx.Database.EnsureDeletedAsync();
                    await dbx.Database.EnsureCreatedAsync();
                }
                else
                {
                    await dbx.Database.MigrateAsync();
                }
            }

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
