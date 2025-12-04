using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Services.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.DomainLayer.Entities;


namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery,ISpecification<TEntity,Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            //Create Query
            var Query = inputQuery;
            // Modify the IQueryable using the specification's criteria
            if ( specification.Criteria != null)
                Query = Query.Where(specification.Criteria);
            // Ordering
            if (specification.OrderBy != null)
                Query = Query.OrderBy(specification.OrderBy);
            if(specification.OrderByDescending != null)
                Query = Query.OrderByDescending(specification.OrderByDescending);
            // Include
            if (specification.Includes != null)
            {
                Query = specification.Includes.Aggregate(Query, (current, include) => current.Include(include));
            }
            if (specification.IsPagingEnabled)
                Query = Query.Skip(specification.Skip).Take(specification.Take);

            return Query;

        }

    }
}
