using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp.Controllers
{
    /// <summary>
    /// Base controller providing common functionality for all controllers.
    /// </summary>
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;

        /// <summary>
        /// Sends a request to the mediator and handles authenticated requests.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request to send to the mediator.</param>
        /// <returns>The response from the mediator.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the mediator is not found in the service provider.</exception>
        protected async Task<TResponse> SendToMediator<TResponse>(IRequest<TResponse> request)
        {
            if (_mediator == null)
            {
                _mediator = HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
            }

            if (_mediator == null)
            {
                throw new InvalidOperationException("Mediator not found in the service provider.");
            }

            if (request is IAuthenticatedRequest<TResponse>)
            {
                AuthenticateRequest(request);
            }

            return await _mediator.Send<TResponse>(request, HttpContext.RequestAborted);
        }

        private void AuthenticateRequest<TResponse>(IRequest<TResponse> request)
        {
            string? userId = User.Identity?.Name;

            if (string.IsNullOrEmpty(userId)) return;

            IEnumerable<Claim> roleClaims = User.FindAll(ClaimTypes.Role);
            string[] roles = roleClaims.Select(c => c.Value).ToArray();

            ((IAuthenticatedRequest<TResponse>)request).SetupRequest(userId, roles);
        }
    }
}
