using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Domain.Repository;
using Talabat.Domain.Specification.Product_Specs;

namespace Talabat.APIs.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> productsRepo;

        public ProductController(IGenericRepository<Product> productRepo)
        {
            productsRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return Ok(await productsRepo.GetAllWithSpecAsync(new ProductWithBrandAndCategorySpecifications()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await productsRepo.GetByIdAsyncWithSpecAsync(new ProductWithBrandAndCategorySpecifications(id));
            if (product == null)
                return NotFound();
            return Ok(product);
        }

    }
}
