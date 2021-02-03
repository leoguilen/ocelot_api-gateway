using Microservicos.CustomerService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microservicos.CustomerService.Data
{
    public class CustomerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options,
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration
                    .GetConnectionString("DefaultConnection"));
            }
        }
    }
}
