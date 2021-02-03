using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Microservicos.HealthCheck
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks()
                .AddSqlServer(
                    connectionString: Configuration["HealthCheckSettings:Databases:0:ConnectionString"],
                    healthQuery: "SELECT 1;",
                    name: Configuration["HealthCheckSettings:Databases:0:Name"],
                    tags: new string[] { "sqlserver" })
                .AddNpgSql(
                    npgsqlConnectionString: Configuration["HealthCheckSettings:Databases:1:ConnectionString"],
                    healthQuery: "SELECT 1;",
                    name: Configuration["HealthCheckSettings:Databases:1:Name"],
                    tags: new string[] { "postgresql" })
                .AddUrlGroup(new Uri(Configuration["HealthCheckSettings:Services:0:Url"]),
                                     Configuration["HealthCheckSettings:Services:0:Name"],
                                     tags: new[] { "api", "microservice" })
                .AddUrlGroup(new Uri(Configuration["HealthCheckSettings:Services:1:Url"]),
                                     Configuration["HealthCheckSettings:Services:1:Name"],
                                     tags: new[] { "api", "microservice" })
                .AddUrlGroup(new Uri(Configuration["HealthCheckSettings:Services:2:Url"]),
                                     Configuration["HealthCheckSettings:Services:2:Name"],
                                     tags: new[] { "seq", "logging" })
                .AddUrlGroup(new Uri(Configuration["HealthCheckSettings:Functions:0:Url"]),
                                     Configuration["HealthCheckSettings:Functions:0:Name"],
                                     tags: new[] { "azure-function", "notification" });

            services
                .AddHealthChecksUI(s =>
                {
                    s.SetEvaluationTimeInSeconds(180);
                    s.AddHealthCheckEndpoint("Microservicos", Configuration["HealthCheckSettings:Endpoint"]);
                })
                .AddInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions() { RequestPath = "/ui/resources" });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI(options =>
                {
                    options.UIPath = "/health-ui";
                });

                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
