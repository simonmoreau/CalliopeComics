using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Series.Queries.GetSerieDetailQuery
{
    public class GetSerieDetailQueryHandlers : IAuthenticatedRequestHandler<GetSerieDetailQuery, SeriesDto>
    {
        private readonly IGcdDbContext _context;
        private readonly ILogger<GetSerieDetailQueryHandlers> _logger;

        public GetSerieDetailQueryHandlers(IGcdDbContext context,
            ILogger<GetSerieDetailQueryHandlers> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SeriesDto> Handle(GetSerieDetailQuery request, CancellationToken cancellationToken)
        {
            GcdSeries? series = await _context.GcdSeries.Where(s => s.Id == request.Id)
                        .Include(s => s.GcdIssues.Where(i => i.VariantOf == null))
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(cancellationToken);

            if (series == null)
            {
                throw new NotFoundException(nameof(GcdSeries), request.Id);
            }

            return new SeriesDto(series);
        }
    }
}
