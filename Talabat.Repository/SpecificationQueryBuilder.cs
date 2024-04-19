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
        public static IQueryable<T> ApplySpecification(IQueryable<T> query, ISpecifications<T> specification)
        {
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }

    }
}
