using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IAuthenticatedRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IDbContext _context;

        public AuthorizationBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request = SetupRequest(request);

            TResponse? response = await next();

            return response;
        }

        private TRequest SetupRequest(TRequest request)
        {
            return request;
        }

    }
}
