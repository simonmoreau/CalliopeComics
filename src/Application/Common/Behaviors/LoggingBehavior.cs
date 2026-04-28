using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Pre-processing
            string name = typeof(TRequest).Name;
            string userId = "unknown";

            if (typeof(IAuthenticatedRequest<TResponse>).IsAssignableFrom(typeof(TRequest)))
            {
                userId = ((IAuthenticatedRequest<TResponse>)request).UserId;
            }

            _logger.LogInformation("Handling {@request} from {@userId}", request, userId);
            Stopwatch stopwatch = Stopwatch.StartNew();

            TResponse? response = await next();

            // Post-processing
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            _logger.LogInformation("Handled {@request} from {@userId} in {@elapsed} ms", request, userId, elapsed);

            return response;
        }
    }
}
