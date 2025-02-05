using Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext()
        {

        }

        public ApiContext(DbContextOptions<ApiContext> context) : base(context)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
