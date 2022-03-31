using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TravelAgency.DAL.Tests
{
    public class DbContextUserTests : IAsyncLifetime
    {
            //Field(variable) for DbContext
        private readonly TravelAgencyDbContext _travelAgencyDbContextSut;

        /**
         * @brief Tests for UserEntity in DbContext - DB
         * 
         */
        public DbContextUserTests()
        {
                //DbContextOption su nastavenia DbContextu, ktore sa nasledne nakofiguruju
                // Pouziva sa Entity framework InMemory - simulovanie DB, data vlozene na testovacie uceli -> neperzistentne data
                //      Treba prerobit na perzistentnu DB s datami
            var dbContextOptions = new DbContextOptionsBuilder<TravelAgencyDbContext>();
            dbContextOptions.UseInMemoryDatabase("TravelAgency");

            _travelAgencyDbContextSut = new TravelAgencyDbContext(dbContextOptions.Options);
            
        }

            //Vsetky databazove komunikacie musia byt ASYNC, inak by UI zamrzol
            //Task je specialny typ, ktory vrati asynchorrna funkcia
            // 2. cviko 3:40:00
            // Await pozastavi priebeh asynchronnej funkcie a caka, kym sa dany prikaz vykona - rovnako ako v Javascripte
        [Fact]
        public async Task GetAll_Users()
        {
            var users = await _travelAgencyDbContextSut.Users.ToArrayAsync();

            Assert.True(!users.Any());
        }

        [Fact]
        public async Task GetAll_Cars()
        {
            var users = await _travelAgencyDbContextSut.Cars.ToArrayAsync();

            Assert.True(!users.Any());
        }


        /**
         * @brief Inicialisation of tests - open connection between DbContext and database
         *
         */
        public async Task InitializeAsync()
        {
            await _travelAgencyDbContextSut.Database.EnsureCreatedAsync();
        }

        /**
         * @brief Destruction of connection between dbContext and database - for correct run
         *
         */
        public async Task DisposeAsync()
        {
            await _travelAgencyDbContextSut.DisposeAsync();
        }
    }
}