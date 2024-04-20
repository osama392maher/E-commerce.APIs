﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Domain.Specification
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public Expression<Func<T, object>>? OrderByDesc { get; set; }


    }
}
