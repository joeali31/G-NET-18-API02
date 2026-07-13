using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Identity;
using E_Commerce01.Application.Services.Contracts;
using E_Commerce01.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Infrastructure.Identity.Services
{
    public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
    {
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                return Result<bool>.Fail(Error.NotFound("user.notfound", "can not found user check email or password"));
            
            var result = await userManager.CheckPasswordAsync(user , password);

            return result ? 
                Result<bool>.Ok(true) 
                :
                Result<bool>.Fail(Error.NotFound("user.notfound", "can not found user check email or password"));
        }

        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("user.notfound" , "can not found user check email or password"));

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id , user.Email! , user.DisplayName , user.UserName!));
        }
        
        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var userCheckEmail = await userManager.FindByEmailAsync(registerDto.Email);

            if (userCheckEmail is not null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("InvalidCredential", "InvalidCredential"));

            var applicationUser = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };

            var creationResult = await userManager.CreateAsync(applicationUser, registerDto.Password);

            if (!creationResult.Succeeded)
            {
                var errors = creationResult.Errors.Select(e => new Error(e.Code , e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(applicationUser.Id, applicationUser.Email, applicationUser.DisplayName, applicationUser.UserName));
        }

    }
}
