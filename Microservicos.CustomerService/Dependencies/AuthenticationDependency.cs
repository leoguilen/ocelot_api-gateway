using Microservicos.CustomerService.Authorization;
using Microservicos.CustomerService.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Microservicos.CustomerService.Dependencies
{
    public static class AuthenticationDependency
    {
        public static void AddAuthenticationDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var signingKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IAuthorizationHandler, UpdateDeleteHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteUpdatePermission", policy =>
                    policy.Requirements.Add(new UpdateDeleteRequirement()));
            });
        }
    }
}
