using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicantTracking.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging request and response information.</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        /// <summary>
        /// Handles the pipeline behavior for logging request and response data.
        /// Logs the request before execution, and logs the response after the request is handled.
        /// If an exception occurs, it logs the error before rethrowing it.
        /// </summary>
        /// <param name="request">The incoming request object.</param>
        /// <param name="next">The delegate representing the next action in the pipeline.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The response from the next action in the pipeline.</returns>
        /// <exception cref="Exception">Throws any exception that occurs while handling the request.</exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Handling request: {RequestName} with data: {@Request}", requestName, request);

            try
            {
                var response = await next();

                _logger.LogInformation("Handled request: {RequestName} with response: {@Response}", requestName, response);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling request: {RequestName}", requestName);
                throw;
            }
        }
    }
}
