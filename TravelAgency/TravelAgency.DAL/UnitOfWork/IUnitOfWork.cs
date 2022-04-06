using TravelAgency.DAL.Entities;

namespace TravelAgency.DAL.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        Task CommitAsync();
    }
}
