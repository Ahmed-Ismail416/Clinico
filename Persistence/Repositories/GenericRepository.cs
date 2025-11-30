using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(ApplicationDbContext _context) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountAsync(ISpecification<TEntity, Tkey> spec)
        => await ApplySpecification(spec).CountAsync();


        public void Delete(TEntity entity)
        => _context.Set<TEntity>().Add(entity);

        public async Task<TEntity?> GetByIdAsync(Tkey id)
        => await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetEntityWithSpec(ISpecification<TEntity, Tkey> spec)
        => await  ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        => await _context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> ListAsyncWithSpec(ISpecification<TEntity, Tkey> spec)
        => await ApplySpecification(spec).ToListAsync();

        public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);


        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, Tkey> spec)
        {
            return SpecificationEvaluator.CreateQuery(_context.Set<TEntity>(), spec);
        }
    }
}
