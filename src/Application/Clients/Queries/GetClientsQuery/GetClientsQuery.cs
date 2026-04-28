using Application.Interfaces;
using Domain.DTO;

namespace Application.Clients.Queries.GetClientsQuery;

public class GetClientsQuery : AuthenticatedRequest<List<ClientDTO>> { }