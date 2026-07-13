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
    public class AuthService(IIdentityService identityService) : IAuthService
    {
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var user = await identityService.FindUserByEmailAsync(loginDto.Email , ct);
            if(!user.IsSuccess)
                return Result<UserDto>.Fail(user.Errors);

            var passwordResult = await identityService.CheckPasswordAsync(loginDto.Email , loginDto.Password , ct);
            if (!passwordResult.IsSuccess)
                return Result<UserDto>.Fail(passwordResult.Errors);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Data.Email,
                DisplayName = user.Data.DisplayName,
                Token = "Token"
            });

        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var createResult = await identityService.CreateUserAsync(registerDto , ct);
            if (!createResult.IsSuccess)
                return Result<UserDto>.Fail(createResult.Errors);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = createResult.Data.Email,
                DisplayName = createResult.Data.DisplayName,
                Token = "Token"
            });
        }
    }
}
