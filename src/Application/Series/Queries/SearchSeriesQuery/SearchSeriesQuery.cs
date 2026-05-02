using Application.Interfaces;
using Domain.DTO;

namespace Application.Series.Queries.SearchSeriesQuery
{
    public class SearchSeriesQuery : AuthenticatedRequest<List<SeriesDto>>
    {
        public readonly string SearchTerm;

        public SearchSeriesQuery(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
