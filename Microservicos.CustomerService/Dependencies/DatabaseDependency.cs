using Microservicos.CustomerService.Data;
using Microservicos.CustomerService.Repository;
using Microservicos.CustomerService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservicos.CustomerService.Dependencies
{
    public static class DatabaseDependency
    {
        public static void AddDbDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql()
                    .AddDbContext<CustomerDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), opt =>
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                    opt.EnableRetryOnFailure(2);
                }));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, Service.CustomerService>();
        }
    }
}
