using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Identity;
using E_Commerce01.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.Services.Calsses
{
    public class AuthService(IIdentityService identityService , IJwtService jwtService) : IAuthService
    {
        public async Task<Result<bool>> CheckEmailExistAsync(string email, CancellationToken ct = default)
        {
            var user = await identityService.FindUserByEmailAsync(email, ct);
            if (!user.IsSuccess) return Result<bool>.Fail(user.Errors);

            return Result<bool>.Ok(true);
        }

        public async Task<Result<AddressDto>> GetCurrentUserAddressDtoAsync(string email, CancellationToken ct = default)
        => await identityService.GetCurrentUserAddressAsync(email, ct);

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {
            var user = await identityService.FindUserByEmailAsync(email, ct);
            if (!user.IsSuccess) return Result<UserDto>.Fail(user.Errors);

            var roles = await identityService.GetUserRolesAsync(email, ct);

            var token = jwtService.CreateTokenAsync(user.Data.Id, user.Data.Email, user.Data.UserName, roles.Data);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Data.Email,
                DisplayName = user.Data.DisplayName,
                Token = token
            });
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var user = await identityService.FindUserByEmailAsync(loginDto.Email , ct);
            if(!user.IsSuccess)
                return Result<UserDto>.Fail(user.Errors);

            var passwordResult = await identityService.CheckPasswordAsync(loginDto.Email , loginDto.Password , ct);
            if (!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(passwordResult.Errors);

            var roles = await identityService.GetUserRolesAsync(user.Data.Email, ct);

            var token = jwtService.CreateTokenAsync(user.Data.Id, user.Data.Email, user.Data.UserName, roles.Data);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Data.Email,
                DisplayName = user.Data.DisplayName,
                Token = token
            });

        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var createResult = await identityService.CreateUserAsync(registerDto , ct);
            if (!createResult.IsSuccess)
                return Result<UserDto>.Fail(createResult.Errors);

            var roles = await identityService.GetUserRolesAsync(createResult.Data.Email, ct);

            var token = jwtService.CreateTokenAsync(createResult.Data.Id, createResult.Data.Email, createResult.Data.UserName, roles.Data);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = createResult.Data.Email,
                DisplayName = createResult.Data.DisplayName,
                Token = token
            });
        }

        public async Task<Result<AddressDto>> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default)
        => await identityService.UpdateCurrentUserAddressAsync (email, addressDto, ct);
    }
}
