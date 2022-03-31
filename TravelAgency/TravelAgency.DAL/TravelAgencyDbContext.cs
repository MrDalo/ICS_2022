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
        }
    }
}
