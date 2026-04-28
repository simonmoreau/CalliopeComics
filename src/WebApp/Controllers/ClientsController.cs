using Application.Clients.Queries.GetClientsQuery;
using Application.Issues.Queries.SearchIssuesQuery;
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

        [HttpGet("issues")]
        public async Task<List<IssueDto>> GetIssues([FromQuery] string searchTerm)
        {
            List<IssueDto> issues = await SendToMediator(new SearchIssuesQuery(searchTerm));
            return issues;
        }
    }
}