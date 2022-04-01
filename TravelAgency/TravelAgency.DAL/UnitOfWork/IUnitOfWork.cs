using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgency.DAL.Entities;
using System.Threading.Tasks;

namespace TravelAgency.DAL.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        Task CommitAsync();
    }
}
