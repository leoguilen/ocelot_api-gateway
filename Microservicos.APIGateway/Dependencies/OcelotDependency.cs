using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Microservicos.APIGateway.Dependencies
{
    public static class OcelotDependency
    {
        public static void AddOcelotDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOcelot();
            services.AddSwaggerForOcelot(configuration);
        }

        public static void UseOcelotSwagger(this IApplicationBuilder app)
        {
            app.UsePathBase("/gateway");

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                opt.PathToSwaggerGenerator = "/swagger/docs";
            })
            .UseOcelot().Wait();
        }
    }
}
