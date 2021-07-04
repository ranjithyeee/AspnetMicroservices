using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _rediscache;

        public BasketRepo(IDistributedCache rediscache)
        {
            _rediscache = rediscache ?? throw new ArgumentNullException(nameof(rediscache));
        }

        public async Task DeleteBasket(string userName)
        {
            await _rediscache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _rediscache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _rediscache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
