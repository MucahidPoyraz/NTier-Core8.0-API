using DTOs.AppRoleDtos;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController : BaseController
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // ✅ Rol Ekle
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateAppRoleDto createAppRoleDto)
        {
            if (string.IsNullOrWhiteSpace(createAppRoleDto.Name))
                return BadRequest("Rol adı boş olamaz.");

            // Hatalı: new IdentityRole(roleName)
            // Doğru: new AppRole(roleName)
            var result = await _roleManager.CreateAsync(new AppRole(createAppRoleDto.Name));
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Rol başarıyla oluşturuldu.");
        }

        // ✅ Tüm Rolleri Listele
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.Select(r => new { r.Id, r.Name }).ToList();
            return Ok(roles);
        }

        // ✏️ Rol Güncelle (sadece isim)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateAppRoleDto updateAppRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(updateAppRoleDto.Id.ToString());
            if (role == null) return NotFound("Rol bulunamadı.");

            role.Name = updateAppRoleDto.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Rol güncellendi.");
        }

        // ❌ Rol Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound("Rol bulunamadı.");

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Rol silindi.");
        }
    }
}
