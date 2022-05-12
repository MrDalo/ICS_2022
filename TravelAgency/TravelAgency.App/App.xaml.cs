using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Windows;
using TravelAgency.App.Extensions;
using TravelAgency.App.Services;
using TravelAgency.App.Services.MessageDialog;
using TravelAgency.App.Settings;
using TravelAgency.App.ViewModels;
using TravelAgency.App.Views;
using TravelAgency.BL;
using TravelAgency.DAL;
using TravelAgency.DAL.Factories;

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
            services.AddSingleton<ISelectOptionViewModel, SelectOptionViewModel>();
            services.AddSingleton<IProfileWindowViewModel, ProfileWindowViewModel>();
            services.AddSingleton<ICreateRideViewModel, CreateRideViewModel>();
            services.AddSingleton<IFilteredRidesViewModel, FilteredRidesViewModel>();
            services.AddSingleton<IUserListViewModel, UserListViewModel>();
            services.AddSingleton<IShareRideListViewModel, ShareRideListViewModel>();
            services.AddSingleton<IPassengerOfShareRideListViewModel, PassengerOfShareRideListViewModel>();
            services.AddSingleton<ICarListViewModel, CarListViewModel>();
            services.AddSingleton<IUserDetailViewModel, UserDetailViewModel>();
            services.AddSingleton<ICarDetailViewModel, CarDetailViewModel>();
            services.AddSingleton<IShareRideDetailViewModel, ShareRideDetailViewModel>();
            services.AddFactory<ISearchRideViewModel, SearchRideViewModel>();
            services.AddFactory<ISelectOptionViewModel, SelectOptionViewModel>();
            services.AddFactory<IPassengerOfShareRideListViewModel, PassengerOfShareRideListViewModel>();
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
