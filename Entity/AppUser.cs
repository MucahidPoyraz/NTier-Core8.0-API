using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
