using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email , CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email , string password , CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto , CancellationToken ct = default);
    }
}
