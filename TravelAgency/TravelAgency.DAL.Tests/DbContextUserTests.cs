using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TravelAgency.DAL.Tests
{
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }
        [Fact]
        public async Task GetAll_Users_FromDB()
        {
            var users = await TravelAgencyDbContextSUT.Users.ToArrayAsync();

            Assert.True(users.Any());
        }
    }
}