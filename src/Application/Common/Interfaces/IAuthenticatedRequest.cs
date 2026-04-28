using MediatR;

namespace Application.Common.Interfaces
{
    public interface IAuthenticatedRequest<TResponse> : IRequest<TResponse>
    {
        public string? UserId { get; }
        public string[] Roles { get; }

        public void SetupRequest(string userId, string[] roles);
    }
}
