using Application.Status.Queries.GetStatusQuery;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/status")]
    public class StatusController : BaseController
    {
        [HttpGet]
        public async Task<StatusDTO> GetStatus()
        {
            StatusDTO status = await SendToMediator(new GetStatusQuery());
            return status;
        }
    }
}