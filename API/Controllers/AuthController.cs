using Common.Interfaces;
using Common.Models;
using DTOs.AppUserDtos;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenManager<AppUser> _tokenManager;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenManager<AppUser> tokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginAppUserDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenManager.GenerateJwtToken(user, roles.ToList());

            return Ok(new
            {
                token,
                user = new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    roles
                }
            });
        }

        // POST: api/Auth/logout
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Çıkış yapıldı.");
        }
    }
}
