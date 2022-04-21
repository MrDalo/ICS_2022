using TravelAgency.DAL.Entities;
using TravelAgency.DAL.Repository;

namespace TravelAgency.DAL.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        Task CommitAsync();
    }
}
