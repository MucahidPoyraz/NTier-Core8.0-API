using DAL.Abstract;
using DAL.Concrete;
using DAL.Context;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Extensions
{
    public static class DalExtension
    {
        public static IServiceCollection LoadDalExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddDbContext<ApiContext>(opt => opt.UseSqlServer(config.GetConnectionString("Local")));
            services.AddScoped<IUow, Uow>();

            return services;
        }
    }
}
