using Application.Common.Interfaces;
using Domain.DTO;
using Microsoft.Extensions.Options;

namespace Application.Settings.Queries
{
    public class GetSettingsQueryHandler : IAuthenticatedRequestHandler<GetSettingsQuery, ApplicationSettings>
    {
        private readonly ApplicationSettings _applicationSettings;

        public GetSettingsQueryHandler(IOptions<ApplicationSettings> settings)
        {
            _applicationSettings = settings.Value;
        }

        public async Task<ApplicationSettings> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            return _applicationSettings;
        }
    }
}
