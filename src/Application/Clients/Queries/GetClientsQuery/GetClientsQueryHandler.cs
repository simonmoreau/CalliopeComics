using Application.Common.Interfaces;
using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients.Queries.GetClientsQuery
{
    public class GetClientsQueryHandler : IAuthenticatedRequestHandler<GetClientsQuery, List<ClientDTO>>
    {
        private readonly IDbContext _context;

        public GetClientsQueryHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientDTO>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            List<CLIENT> clients = await _context.CLIENTS
                .ToListAsync(cancellationToken);

            return clients.Select(client => new ClientDTO(client)).ToList();
        }
    }
}