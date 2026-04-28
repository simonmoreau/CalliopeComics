using Application.Common.Interfaces;

namespace Application.Interfaces
{
    public class AuthenticatedRequest<TResponse> : IAuthenticatedRequest<TResponse>
    {
        private bool _isAuthenticated = false;
        public void SetupRequest(string userId, string[] roles)
        {
            UserId = userId;
            Roles = roles;

            _isAuthenticated = true;
        }

        public string? UserId { get; private set; }
        public string[] Roles { get; private set; } = [];

    }
}
