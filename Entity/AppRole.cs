using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base() { }

        public AppRole(string roleName) : base(roleName) { }
    }
}
