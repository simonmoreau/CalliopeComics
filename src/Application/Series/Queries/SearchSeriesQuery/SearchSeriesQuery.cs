using Application.Interfaces;
using Domain.DTO;

namespace Application.Series.Queries.SearchSeriesQuery
{
    public class SearchSeriesQuery : AuthenticatedRequest<List<SeriesDto>>
    {
        public readonly string SearchTerm;
        public readonly DateTime? Date;

        public SearchSeriesQuery(string searchTerm, DateTime? date = null)
        {
            SearchTerm = searchTerm;
            Date = date;
        }
    }
}
