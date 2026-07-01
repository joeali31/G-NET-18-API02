using E_Commerce01.Domain.Entities.Base;
using E_Commerce01.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Infrastructure.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery , ISpecifications<TEntity , TKey> specs ) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            if (specs.Criteria is not null)
            {
                query = query.Where(specs.Criteria);
            }

            if (specs.Includes.Any())
            {
                foreach (var expression in specs.Includes)
                {
                    query = query.Include(expression);
                }
            }

            if (specs.OrderBy is not null)
            {
                query = query.OrderBy(specs.OrderBy);
            }
            else if (specs.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specs.OrderByDescending);
            }

            if (specs.IsPagingEnabled)
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            }

            return query;
        }
    }
}
