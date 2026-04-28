using Application.Interfaces;
using Domain.DTO;

namespace Application.Settings.Queries
{
    public class GetSettingsQuery : AuthenticatedRequest<ApplicationSettings> { }
}
