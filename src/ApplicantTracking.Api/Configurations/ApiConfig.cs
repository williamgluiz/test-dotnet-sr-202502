using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicantTracking.Api.Configurations
{
    public static class ApiConfig
    {
        /// <summary>
        /// Adds and configures core API services including controllers, API explorer for endpoints,
        /// and a CORS policy named "Development" that allows any origin, method, and header.
        /// </summary>
        /// <param name="services">The service collection to add the API services to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            
            return services;
        }

        /// <summary>
        /// Configures the application's HTTP request pipeline, including HTTPS redirection,
        /// routing, CORS policy, authorization, and endpoint mapping.
        /// Also sets up a redirect from the root URL to the Swagger UI.
        /// </summary>
        /// <param name="app">The application builder to configure the middleware pipeline.</param>
        /// <returns>The configured application builder.</returns>
        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Development");
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger/index.html");
                    return Task.CompletedTask;
                });
            });
            return app;
        }
    }
}
