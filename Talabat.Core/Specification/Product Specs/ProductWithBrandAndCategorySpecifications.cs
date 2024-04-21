using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Domain.Specification.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {

        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
            : base(x => (!specParams.BrandId.HasValue || x.BrandId == specParams.BrandId) &&
                            (!specParams.CategoryId.HasValue || x.CategoryId == specParams.CategoryId))
        {
            Includes.Add(x => x.Brand);
            Includes.Add(x => x.Category);

            if (!string.IsNullOrEmpty(specParams.Sort))
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        OrderBy = P => P.Price;
                        break;
                    case "priceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                    case "nameDesc":
                        OrderByDesc = P => P.Name;
                        break;
                    default:
                        OrderBy = P => P.Name;
                        break;
                }
            else
                OrderBy = P => P.Name;


            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        }
        public ProductWithBrandAndCategorySpecifications()
        {
            Includes.Add(x => x.Brand);
            Includes.Add(x => x.Category);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(x => x.Brand);
            Includes.Add(x => x.Category);
        }
    }
}