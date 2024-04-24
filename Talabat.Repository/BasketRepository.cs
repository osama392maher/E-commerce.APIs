using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.Repository;
using StackExchange.Redis;
using System.Text.Json;
namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase redisDb;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            redisDb = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await redisDb.KeyDeleteAsync(basketId);
        }

        public async Task<UserBasket?> GetBasketAsync(string basketId)
        {
            var basket = await redisDb.StringGetAsync(basketId);
            return basket.IsNullOrEmpty? null : JsonSerializer.Deserialize<UserBasket>(basket);
        }

        public async Task<UserBasket?> UpdateBasketAsync(UserBasket basket)
        {
            var created = await redisDb.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
