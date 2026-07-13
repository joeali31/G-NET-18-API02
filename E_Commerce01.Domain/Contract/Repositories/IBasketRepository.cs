using E_Commerce01.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Domain.Contract.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string BasketId , CancellationToken ct = default);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket , TimeSpan? TimeToLive = default , CancellationToken ct = default);
        Task<bool> DeleteBasketAsync(string BasketId, CancellationToken ct = default);
    }
}
