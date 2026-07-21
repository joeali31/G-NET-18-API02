using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Contracts
{
    public interface IAuthService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto , CancellationToken ct =default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto , CancellationToken ct =default);
        Task<Result<bool>> CheckEmailExistAsync(string email, CancellationToken ct =default);
        Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct =default);
        Task<Result<AddressDto>> GetCurrentUserAddressDtoAsync(string email , CancellationToken ct = default);

        Task<Result<AddressDto>> UpdateCurrentUserAddressAsync(string email , AddressDto addressDto , CancellationToken ct =default);

    }
}
