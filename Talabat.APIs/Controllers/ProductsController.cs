using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Domain.Repository;
using Talabat.Domain.Specification.Product_Specs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Talabat.APIs.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductBrand> brandsRepo;
        private readonly IGenericRepository<ProductCategory> categoriesRepo;
        private readonly IMapper mapper;

        public ProductsController
            (IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductCategory> categoriesRepo,
            IMapper mapper)
        {
            productsRepo = productRepo;
            this.brandsRepo = brandsRepo;
            this.categoriesRepo = categoriesRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<IReadOnlyList<ProductToReturnDTO>>>> GetAllProducts([FromQuery] ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams);

            var prodcuts = await productsRepo.GetAllWithSpecAsync(specs);

            var productsToReturn = mapper.Map<IReadOnlyList<ProductToReturnDTO>>(prodcuts);

            return Ok(new Pagination<ProductToReturnDTO>(specParams.PageIndex, specParams.PageSize, specs.AllCount, productsToReturn));
        }

        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int id)
        {
            var product = await productsRepo.GetByIdAsyncWithSpecAsync(new ProductWithBrandAndCategorySpecifications(id));

            if (product is null)
                return NotFound(new ApiResponse(404)); //404

            var productsToReturn = mapper.Map<ProductToReturnDTO>(product);
            return Ok(productsToReturn);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await categoriesRepo.GetAllAsync();
            return Ok(categories);
        }
    }
}
