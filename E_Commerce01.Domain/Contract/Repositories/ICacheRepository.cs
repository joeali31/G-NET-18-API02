using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Domain.Contract.Repositories
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key , CancellationToken ct = default);
        Task SetAsync(string key, object value , TimeSpan? duration = default , CancellationToken ct = default);
    }
}
