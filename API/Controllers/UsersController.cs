using DTOs.AppUserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // ✅ Yeni Kullanıcı Ekle
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Kullanıcı başarıyla oluşturuldu.");
        }

        // ✅ Kullanıcı Güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            user.Email = dto.Email;
            user.UserName = dto.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Kullanıcı bilgileri güncellendi.");
        }

        // ❌ Kullanıcı Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Kullanıcı silindi.");
        }
    }
}
