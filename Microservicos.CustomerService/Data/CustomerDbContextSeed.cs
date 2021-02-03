using Microservicos.CustomerService.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Data
{
    public class CustomerDbContextSeed
    {
        public static async Task SeedAsync(CustomerDbContext context,
            ILogger<CustomerDbContextSeed> logger)
        {
            try
            {
                await CreateCustomers(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(CustomerDbContextSeed));
            }
        }

        private static async Task CreateCustomers(CustomerDbContext context)
        {
            var customers = new List<Customer>
            {
                
            };

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }
}
