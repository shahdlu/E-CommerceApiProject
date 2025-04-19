using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal static class SpecificationEvalutor
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (specifications.criteria is not null)
            {
                query = query.Where(specifications.criteria);
            }
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                //foreach (var exp in specifications.IncludeExpressions)
                //{
                //    query = query.Include(exp);
                //}
                query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }
            return query;
        }
    }
}
