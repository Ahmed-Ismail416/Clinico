using Core.DomainLayer.Entities;
using DomainLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class BaseSpecification<TEntity , Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
      
        public BaseSpecification(Expression<Func<TEntity,bool>>? _Criteria)
        {
            Criteria = _Criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = []; 

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }


        // AddInclude
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        // AddOrderBy
        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        // AddOrderByDescending
        public void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        // ApplyPaging  
        public void ApplyPaging(int pagesize, int pageindex)
        {
            Skip = (pageindex - 1) * pagesize;
            Take = pagesize;
            IsPagingEnabled = true;
        }
    }
}
