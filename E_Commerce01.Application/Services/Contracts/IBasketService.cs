using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Contracts
{
    public interface IBasketService
    {
        Task<Result<CustomerBasketDto>> GetBasketAsync(string id , CancellationToken cancellationToken = default);
        Task<Result<CustomerBasketDto>> CreateOrUpdateBasketAsync(CustomerBasketDto basketDto, TimeSpan? TimeToLive = default , CancellationToken ct = default);
        Task<Result<bool>> DeleteBasketAsync(string id , CancellationToken cancellationToken = default);

    }
}
