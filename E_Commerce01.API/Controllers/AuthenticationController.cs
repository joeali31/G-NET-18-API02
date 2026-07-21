using E_Commerce01.Application.DTOs.Identity;
using E_Commerce01.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce01.API.Controllers
{
    
    public class AuthenticationController(IAuthService authService) : ApiBaseController
    {

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto , CancellationToken ct = default)
        {
            var result = await authService.LoginAsync(loginDto, ct);
            return ToActionResult(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto , CancellationToken ct = default)
        {
            var result = await authService.RegisterAsync(registerDto, ct);
            return ToActionResult(result);
        }

        [HttpGet("emailExists/{email}")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckEmail(string email, CancellationToken ct = default)
        {
            var result = await authService.CheckEmailExistAsync(email, ct);
            return ToActionResult(result);
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();
            var result = await authService.GetCurrentUserAsync(email, ct);
            return ToActionResult(result);
        }


        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress(CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();
            var result = await authService.GetCurrentUserAddressDtoAsync(email, ct);
            return ToActionResult(result);
        }


        [HttpPost("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress( AddressDto dto , CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new UnauthorizedAccessException();
            var result = await authService.UpdateCurrentUserAddressAsync(email, dto, ct);
            return ToActionResult(result);
        }




    }
}
