using BL.Concrete;
using BL.ValidationRules;
using Common.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<BlogValidation>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidation>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]))
                };
            });

            // Role bazlı doğrulama için Authorization servisi
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            return services;
        }
    }
}
