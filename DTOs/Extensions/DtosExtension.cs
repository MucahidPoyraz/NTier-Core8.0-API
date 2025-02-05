using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DTOs.Extensions
{
    public static class DtosExtension
    {
        public static IServiceCollection LoadDtosExtensions(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
