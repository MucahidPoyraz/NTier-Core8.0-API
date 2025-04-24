using Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.Context
{
    public class ApiContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public ApiContext(DbContextOptions<ApiContext> context) : base(context)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
