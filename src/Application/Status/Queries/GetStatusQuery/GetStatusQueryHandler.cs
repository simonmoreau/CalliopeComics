using Application.Common.Interfaces;
using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Security.Principal;

namespace Application.Status.Queries.GetStatusQuery
{
    public class GetStatusQueryHandler : IAuthenticatedRequestHandler<GetStatusQuery, StatusDTO>
    {
        private readonly IDbContext _context;
        private readonly ApplicationSettings _applicationSettings;

        public GetStatusQueryHandler(IDbContext context, IOptions<ApplicationSettings> settings)
        {
            _context = context;
            _applicationSettings = settings.Value;
        }

        public async Task<StatusDTO> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            string? version = Assembly.GetAssembly(typeof(GetStatusQueryHandler))?.GetName().Version?.ToString();
            bool isConnected = ((DbContext)_context).Database.CanConnect();
            bool isAuthentificated = request.UserId != null;

            Assembly? assembly = Assembly.GetAssembly(typeof(GetStatusQueryHandler));
            string? location = assembly?.Location;
            string? directory = Path.GetDirectoryName(location);

            return new StatusDTO
            {
                Version = version,
                DatabaseConnected = isConnected,
                PathAssembly = directory
            };
        }
    }
}
