using Application.Settings.Queries;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : BaseController    
    {
        [HttpGet]
        public async Task<ApplicationSettings> GetSettings()
        {
            ApplicationSettings settings = await SendToMediator(new GetSettingsQuery());
            return settings;
        }
    }
}