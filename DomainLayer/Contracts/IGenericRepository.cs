using Core.DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<IReadOnlyList<TEntity>> ListAsyncWithSpec(ISpecification<TEntity, Tkey> spec);
        Task<TEntity?> GetEntityWithSpec(ISpecification<TEntity, Tkey> spec);
        Task<int> CountAsync(ISpecification<TEntity, Tkey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);



    }
}
