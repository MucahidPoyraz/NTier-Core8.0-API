using BL.Abstract;
using BL.Concrete;
using BL.ValidationRules;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BL.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection LoadServiceExtetion(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddScoped(typeof(IGenericManager<>), typeof(GenericManager<>));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<BlogValidation>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidation>();

            return services;
        }
    }
}
