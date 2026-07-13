using E_Commerce01.Domain.Contract.Repositories;
using E_Commerce01.Domain.Entities.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce01.Infrastructure.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase(); // in memory database

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            var value = JsonSerializer.Serialize(customerBasket);
            var result = await _database.StringSetAsync(customerBasket.Id, value, TimeToLive ?? TimeSpan.FromDays(7));

            return result ? customerBasket : null;
        }

        public async Task<bool> DeleteBasketAsync(string BasketId, CancellationToken ct = default)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId, CancellationToken ct = default)
        {
            var result = await _database.StringGetAsync(BasketId);

            if(result.IsNullOrEmpty) return null;

            var value = JsonSerializer.Deserialize<CustomerBasket>(result);
            if(value is null) return null;

            return value;
        }
    }
}
