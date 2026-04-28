using MediatR;

namespace Application.Common.Interfaces
{
    public interface IAuthenticatedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IAuthenticatedRequest<TResponse>
    {
    }
}
