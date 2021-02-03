using Microservicos.AuthenticationService.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager,
            ILogger<AppIdentityDbContextSeed> logger)
        {
            try
            {
                await CreateRoles(roleManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(AppIdentityDbContext));
            }
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = new IdentityRole(Roles.ADMINISTRADOR);
            var operadorRole = new IdentityRole(Roles.OPERADOR);
            var visualizadorRole = new IdentityRole(Roles.VISUALIZADOR);

            await roleManager.CreateAsync(adminRole);
            await roleManager.CreateAsync(operadorRole);
            await roleManager.CreateAsync(visualizadorRole);
        }
    }
}
