using Application.Clients.Queries.GetClientsQuery;
using Application.Issues.Queries.GetIssueDetailsQuery;
using Domain.Entities;
using Application.Issues.Queries.SearchIssuesQuery;
using Application.Series.Queries.SearchSeriesQuery;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Series.Queries.GetSerieDetailQuery;
using Application.ComicInfo.Command.SetComicInfoDetailCommand;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : BaseController
    {
        [HttpGet("issues/{id}")]
        public async Task<GcdIssue> GetIssueDetails([FromRoute] int id)
        {
            GcdIssue issue = await SendToMediator(new GetIssueDetailsQuery(id));
            return issue;
        }

        [HttpGet("issues/{id}/write")]
        public async Task WriteIssueDetails([FromRoute] int id, [FromQuery] string path, [FromQuery] string? seriesGroup = null)
        {
            await SendToMediator(new SetComicInfoDetailCommand(id, path, seriesGroup));
            return;
        }

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


        [HttpGet("series")]
        public async Task<List<SeriesDto>> GetSeries([FromQuery] string searchTerm)
        {
            List<SeriesDto> series = await SendToMediator(new SearchSeriesQuery(searchTerm));
            return series;
        }

        [HttpGet("comicinfo")]
        public async Task<string?> GetComicInfoDetail([FromQuery] string path, [FromQuery] string propertyName)
        {
            return await SendToMediator(new GetComicInfoDetailQuery(path, propertyName));
        }

        [HttpGet("series/{id}")]
        public async Task<SeriesDto> GetSeriesDetails([FromRoute] int id)
        {
            SeriesDto series = await SendToMediator(new GetSerieDetailQuery(id));
            return series;
        }
    }
}
