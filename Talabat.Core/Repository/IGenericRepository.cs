using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Domain.Specification;

namespace Talabat.Domain.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        #region Without Specifications
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        #endregion

        #region With Specifications
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetByIdAsyncWithSpecAsync(ISpecifications<T> spec);
        #endregion

    }
}
