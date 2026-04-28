using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    public class RequestGenericExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException> 
        where TException : Exception
    {
        private readonly ILogger<TRequest> _logger;
        public RequestGenericExceptionHandler(ILogger<TRequest> logger)
        {
            _logger = logger;
        }
        public async Task Handle(TRequest request, TException exception,
                                 RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            // Pre-processing
            string name = typeof(TRequest).Name;
            string userId = "unknown";

            if (typeof(IAuthenticatedRequest<TResponse>).IsAssignableFrom(typeof(TRequest)))
            {
                userId = ((IAuthenticatedRequest<TResponse>)request).UserId;
            }

            _logger.LogError(exception, "Request Exception {@name} {@message} {@userId}", name, exception.Message, userId);

            await Task.CompletedTask;
        }
    }
}