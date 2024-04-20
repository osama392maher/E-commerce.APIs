using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Domain.Specification;

namespace Talabat.Repository
{
    public static class SpecificationQueryBuilder<T> where T : BaseEntity
    {
        public static IQueryable<T> ApplySpecification(IQueryable<T> query, ISpecifications<T> spec)
        {
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            return query;
        }

    }
}
