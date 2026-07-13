using E_Commerce01.Application.Services.Contracts;
using E_Commerce01.Domain.Contract.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Calsses
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key, CancellationToken ct = default)
            => await cacheRepository.GetAsync(key, ct);

        public async Task SetAsync(string key, object value, TimeSpan? duration = default, CancellationToken ct = default)
        => await cacheRepository.SetAsync(key, value, duration, ct);
    }
}
