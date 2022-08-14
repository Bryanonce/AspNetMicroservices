using Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redis;

        public BasketRepository(IDistributedCache redis)
        {
            this.redis = redis;
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            await redis.RemoveAsync(userName);
            return GetBasket(userName) == null;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basquet = await redis.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basquet)) return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basquet);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await redis.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
