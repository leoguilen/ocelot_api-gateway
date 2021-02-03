using Microservicos.AuthenticationService.CustomTokenProviders;
using Microservicos.AuthenticationService.Data;
using Microservicos.AuthenticationService.Domain;
using Microservicos.AuthenticationService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microservicos.AuthenticationService.Dependencies
{
    public static class IdentityDependency
    {
        public static void AddIdentityDbDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt =>
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                    opt.EnableRetryOnFailure(2);
                }));
        }

        public static void AddIdentityDependency(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = true;

                options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.Tokens.EmailConfirmationTokenProvider = "email-confirmation-token";
                options.Tokens.PasswordResetTokenProvider = "password-reset-token";
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("email-confirmation-token")
                .AddTokenProvider<PasswordResetTokenProvider<ApplicationUser>>("password-reset-token");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(2));
            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromDays(3));
            services.Configure<PasswordResetTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromDays(3));

            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
