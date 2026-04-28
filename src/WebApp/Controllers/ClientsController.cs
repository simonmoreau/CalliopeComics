using Application.Clients.Queries.GetClientsQuery;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : BaseController
    {
        [HttpGet]
        public async Task<List<ClientDTO>> GetClients()
        {
            List<ClientDTO> clients = await SendToMediator(new GetClientsQuery());
            return clients;
        }
    }
}