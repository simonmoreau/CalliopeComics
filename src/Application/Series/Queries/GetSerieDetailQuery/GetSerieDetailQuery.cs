using Application.Interfaces;
using Domain.DTO;

namespace Application.Series.Queries.GetSerieDetailQuery
{
    public class GetSerieDetailQuery : AuthenticatedRequest<SeriesDto>
    {
        public readonly int Id;
        public GetSerieDetailQuery(int id)
        {
            Id = id;
        }
    }
}
