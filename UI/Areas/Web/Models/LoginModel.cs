using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; }
    }
}
