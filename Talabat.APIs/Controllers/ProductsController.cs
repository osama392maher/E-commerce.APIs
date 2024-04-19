using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Domain.Repository;
using Talabat.Domain.Specification.Product_Specs;

namespace Talabat.APIs.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
        {
            productsRepo = productRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetAllProducts()
        {
            var prodcuts = await productsRepo.GetAllWithSpecAsync(new ProductWithBrandAndCategorySpecifications());

            var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDTO>>(prodcuts);

            return Ok(productsToReturn);
        }

        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProductById(int id)
        {
            var product = await productsRepo.GetByIdAsyncWithSpecAsync(new ProductWithBrandAndCategorySpecifications(id));

            if (product == null)
                return NotFound(new ApiResponse(404)); //404

            var productsToReturn = mapper.Map<ProductToReturnDTO>(product);
            return Ok(productsToReturn);
        }

    }
}
