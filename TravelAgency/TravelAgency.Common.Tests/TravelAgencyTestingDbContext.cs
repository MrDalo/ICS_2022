using TravelAgency.Common.Tests.Seeds;
using TravelAgency.DAL;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Common.Tests
{
    public class CookBookTestingDbContext : TravelAgencyDbContext
    {
        private readonly bool _seedTestingData;

        public CookBookTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
            : base(contextOptions, seedDemoData: false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_seedTestingData)
            {
                //TODO
                /* IngredientSeeds.Seed(modelBuilder);
                 RecipeSeeds.Seed(modelBuilder);
                 IngredientAmountSeeds.Seed(modelBuilder);*/
            }
        }
    }
}