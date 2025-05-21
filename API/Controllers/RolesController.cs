using API.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // ✅ Rol Ekle
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Rol adı boş olamaz.");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
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
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string newName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound("Rol bulunamadı.");

            role.Name = newName;
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