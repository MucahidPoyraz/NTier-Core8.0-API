using DAL.Concrete;
using DAL.Context;
using DAL.UnitOfWork;
using Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Common.Interfaces;

namespace DAL.Extensions
{
    public static class DalExtension
    {
        public static IServiceCollection LoadDalExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // AppRole yerine IdentityRole yerine kendi özelleştirilmiş role sınıfınızı kullanın
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                // Şifre, kullanıcı vs. gibi temel ayarları buraya ekleyebilirsiniz
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApiContext>() // AppDbContext kullanıldığı varsayılıyor
            .AddDefaultTokenProviders();

            services.AddScoped<IUOW, Uow>();

            return services;
        }
    }
}
