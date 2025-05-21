using BL.Concrete;
using BL.ValidationRules;
using Common.Interfaces;
using Common.Models;
using Entity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Reflection;
using System.Text;

namespace BL.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection LoadServiceExtetion(this IServiceCollection services, IConfiguration config)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            services.AddScoped(typeof(IGenericManager<>), typeof(GenericManager<>));
            services.AddScoped<ITokenManager<AppUser>, JwtTokenService>();
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<BlogValidation>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidation>();          

            LogManager.Setup().LoadConfigurationFromFile("nlog.config");

            return services;
        }
    }
}
