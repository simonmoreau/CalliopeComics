using Application.Interfaces;
using Domain.DTO;

namespace Application.Status.Queries.GetStatusQuery
{
    public class GetStatusQuery : AuthenticatedRequest<StatusDTO> { }
}