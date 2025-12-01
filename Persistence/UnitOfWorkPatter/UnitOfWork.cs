using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using Microsoft.Identity.Client;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.UnitOfWorkPatter
{
    public class UnitOfWork(ApplicationDbContext _dbcontext) : IUnitOfWork
    {
        public readonly Dictionary<string, object> repos = [];
        public IGenericRepository<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (repos.TryGetValue(typeName, out object repo))
            {
                return (IGenericRepository<TEntity, TKey> )repo;
            }
            else
            {
                var newRepo= new GenericRepository<TEntity, TKey>(_dbcontext);
                repos.Add(typeName, newRepo);
                return newRepo;
            }
        }

        public Task<int> SaveChangesAsync()
            => _dbcontext.SaveChangesAsync();
    }
}
