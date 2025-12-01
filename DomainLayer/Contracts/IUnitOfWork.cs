using Core.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepository<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    
        Task<int> SaveChangesAsync();

    }
}
