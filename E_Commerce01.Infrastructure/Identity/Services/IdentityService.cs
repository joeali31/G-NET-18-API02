using E_Commerce01.Application.Common;
using E_Commerce01.Application.DTOs.Identity;
using E_Commerce01.Application.Services.Contracts;
using E_Commerce01.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                return Result<IReadOnlyList<string>>.Fail(Error.NotFound("InvalidCredential", "InvalidCredential"));

            var roles = await userManager.GetRolesAsync(user);

            return Result<IReadOnlyList<string>>.Ok(roles.ToList());
        }

        public async Task<Result<AddressDto>> GetCurrentUserAddressAsync(string email, CancellationToken ct = default)
        {
            var user = await userManager.Users.Include(u => u.Address).Where(u => u.Email == email).FirstOrDefaultAsync(ct);
            if (user is null)
                return Result<AddressDto>.Fail(Error.NotFound("user.notfound", "can not found user check email or password"));

            if (user.Address is null)
                return Result<AddressDto>.Fail(Error.NotFound("Address.Notfound" , "Address is not exist"));

            return Result<AddressDto>.Ok(new AddressDto(user.Address.Street , user.Address.City , user.Address.Country , user.Address.FirstName , user.Address.LastName));
        }

        public async Task<Result<AddressDto>> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default)
        {
            var user = await userManager.Users.Include(u => u.Address).Where(u => u.Email == email).FirstOrDefaultAsync(ct);
            if (user is null)
                return Result<AddressDto>.Fail(Error.NotFound("user.notfound", "can not found user check email or password"));
            if (user.Address is null)
            {
                user.Address = new Address()
                {
                    City = addressDto.City,
                    Country = addressDto.Country,
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName,
                    Street = addressDto.Street
                };

            }
            else
            {
                user.Address.Street = addressDto.Street;
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Country = addressDto.Country;
                user.Address.City = addressDto.City;
            }

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Result<AddressDto>.Fail(Error.Failure("Failure" , "Can not update or create address"));

            return Result<AddressDto>.Ok(addressDto);
        }
    }
}
