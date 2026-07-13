using E_Commerce01.Application.DTOs.Identity;
using E_Commerce01.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
