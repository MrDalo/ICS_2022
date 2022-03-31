using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelAgency.DAL.Entities;

namespace TravelAgency.DAL
{
    public class TravelAgencyDbContext : DbContext
    {
        /**
         * @brief Construction of DbContext
         */
        public TravelAgencyDbContext(DbContextOptions contextOptions): base(contextOptions)
        {

        }


        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<ShareRideEntity> ShareRides => Set<ShareRideEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

            //Vlozenie pociatocnych dat, ak sa DB spusti, budu tam testovacie data - isiel som podla cvika 2.
            //Nedokoncil som to preto, lebo nas DB ma byt prezistentna(data sa nemaju po vypnuti appky stratil a maju sa zachovat)
            //Teda neviem ,ci mame Db naplnit pre testovacie uceli datami dopredu alebo v testoch sa data insertnu, otestuju a nasledne zmazu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarEntity>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Cars)
                .HasForeignKey( c =>c.OwnerId);


            modelBuilder.Entity<ShareRideEntity>()
                .HasOne(s => s.Car)
                .WithMany() // Neviem ci toto pojde, CarEntity nema iCollection<ShareRideEntity> takze to nemam s cim prepojit
                .HasForeignKey(s => s.CarId);

            modelBuilder.Entity<ShareRideEntity>()
                .HasOne(s => s.Driver)
                .WithMany( u => u.DriverShareRides) 
                .HasForeignKey(s => s.DriverId);
                //TODO hasForeignKey treba asi zmenit na nejaku implicitnu hodnotu, aby to tam nebolo natvrdo.... hovoril v prednaske, bude mozno na cviku

            modelBuilder.Entity<ShareRideEntity>()
                .HasMany(s => s.Passengers)
                .WithMany(u => u.PassengerShareRides)
                .UsingEntity(j => j.ToTable("PassengerOfShareRide")); // Malo by to vytvorit novu tabulku s nazvom PassengerOfShareRide - N k N vztah je tvorba novej tabulky
                //src: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-composite-key%2Csimple-key
        }
    }
}
