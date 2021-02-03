using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace Microservicos.AuthenticationService.Dependencies
{
    public static class SwaggerDependency
    {
        public static void AddSwaggerDependency(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Authentication API",
                    Version = "v1",
                    Description = "API para autenticação dos usuário e gerar tokens de acesso",
                    Contact = new OpenApiContact
                    {
                        Email = "leonardoguilen1@gmail.com",
                        Name = "Leonardo Guilen"
                    }
                });

                options.ExampleFilters();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }
        public static void UseSwaggerDependency(this IApplicationBuilder app)
        {
            app.UseSwagger(options =>
                options.RouteTemplate = "{documentName}/swagger.json");

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "";
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.SwaggerEndpoint("v1/swagger.json", "Authentication API - v1");
            });
        }
    }
}
