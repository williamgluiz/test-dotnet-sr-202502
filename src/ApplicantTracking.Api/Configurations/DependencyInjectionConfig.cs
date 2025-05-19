using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Infrastructure.Repositories;
using ApplicantTracking.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicantTracking.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Registers the main application dependencies in the dependency injection container.
        /// </summary>
        /// <param name="services">The service collection to add the dependencies to.</param>
        /// <returns>The service collection with the registered dependencies.</returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ITimelineRepository, TimelineRepository>();
            return services;
        }
    }
}
