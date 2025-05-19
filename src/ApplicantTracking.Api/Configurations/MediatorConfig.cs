using ApplicantTracking.Application.Behaviors;
using ApplicantTracking.Application.Commands.Candidate.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicantTracking.Api.Configurations
{
    public static class MediatorConfig
    {
        /// <summary>
        /// Configures MediatR and registers FluentValidation validators for the application.
        /// </summary>
        /// <param name="services">The IServiceCollection instance used for dependency injection.</param>
        /// <returns>The updated IServiceCollection with MediatR and validation services registered.</returns>
        public static IServiceCollection AddMediatorConfig(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCandidateCommand).Assembly));

            services.AddValidatorsFromAssemblyContaining<CreateCandidateValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
