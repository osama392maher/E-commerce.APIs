using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Domain.Entities;
using Talabat.Domain.Repository;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserBasket>> GetBasketById(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new UserBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<UserBasket>> UpdateBasket(UserBasketDto basket)
        {
            var mappedBasket = mapper.Map<UserBasketDto, UserBasket>(basket);
            var updatedBasket = await basketRepository.UpdateBasketAsync(mappedBasket);
            if (updatedBasket is null)
                return BadRequest("Problem updating the basket");
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await basketRepository.DeleteBasketAsync(id);
        }
    }
}
