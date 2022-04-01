using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IDbContextFactory<TravelAgencyDbContext> _dbContextFactory;

        public UnitOfWorkFactory(IDbContextFactory<TravelAgencyDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
    }
}
