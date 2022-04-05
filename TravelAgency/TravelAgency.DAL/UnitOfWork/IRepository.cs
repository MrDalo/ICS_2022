using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using TravelAgency.DAL.Entities;

namespace TravelAgency.DAL.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Get();
        void Delete(Guid entityId);
        Task<TEntity> InsertOrUpdateAsync<TModel>(
            TModel model,
            IMapper mapper,
            CancellationToken cancellationToken = default) where TModel : class;
    }
}
