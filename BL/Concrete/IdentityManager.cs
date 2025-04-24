using Common.Enums;
using Common.ResponseModels;
using Entity;
using Microsoft.AspNetCore.Identity;

namespace BL.Concrete
{
    public class IdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public IdentityService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Kullanıcı ekleme
        public async Task<TResponse<AppUser>> CreateUserAsync(AppUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return new TResponse<AppUser>
                {
                    Data = user,
                    Message = "Kullanıcı başarıyla oluşturuldu.",
                    ResponseType = ResponseType.Success
                };
            }
            else
            {
                return new TResponse<AppUser>
                {
                    Message = "Kullanıcı oluşturulamadı: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                    ResponseType = ResponseType.Error
                };
            }
        }

        // Kullanıcı rolü ekleme
        public async Task<TResponse<AppUser>> AddRoleToUserAsync(AppUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return new TResponse<AppUser>
                {
                    Data = user,
                    Message = "Rol başarıyla eklendi.",
                    ResponseType = ResponseType.Success
                };
            }
            else
            {
                return new TResponse<AppUser>
                {
                    Message = "Rol eklenemedi: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                    ResponseType = ResponseType.Error
                };
            }
        }

        // Kullanıcıyı bulma
        public async Task<TResponse<AppUser>> FindUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new TResponse<AppUser>
                {
                    Data = user,
                    Message = "Kullanıcı başarıyla bulundu.",
                    ResponseType = ResponseType.Success
                };
            }
            else
            {
                return new TResponse<AppUser>
                {
                    Message = "Kullanıcı bulunamadı.",
                    ResponseType = ResponseType.Error
                };
            }
        }

        // Kullanıcı giriş işlemi (Password doğrulama)
        public async Task<TResponse<bool>> CheckPasswordAsync(AppUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return new TResponse<bool>
            {
                Data = result,
                Message = result ? "Giriş başarılı." : "Şifre hatalı.",
                ResponseType = result ? ResponseType.Success : ResponseType.Error
            };
        }

        // Rol ekleme
        public async Task<TResponse<AppRole>> CreateRoleAsync(string roleName)
        {
            var role = new AppRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new TResponse<AppRole>
                {
                    Data = role,
                    Message = "Rol başarıyla oluşturuldu.",
                    ResponseType = ResponseType.Success
                };
            }
            else
            {
                return new TResponse<AppRole>
                {
                    Message = "Rol oluşturulamadı: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                    ResponseType = ResponseType.Error
                };
            }
        }

        // Kullanıcıyı silme
        public async Task<TResponse<bool>> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return new TResponse<bool>
            {
                Data = result.Succeeded,
                Message = result.Succeeded ? "Kullanıcı başarıyla silindi." : "Kullanıcı silinemedi: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                ResponseType = result.Succeeded ? ResponseType.Success : ResponseType.Error
            };
        }
    }
}
