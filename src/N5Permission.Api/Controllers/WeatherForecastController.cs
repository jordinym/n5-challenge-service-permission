using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using N5Permission.Application.Permission.Queries.GetPermissions;
using N5Permission.Application.Permission.Queries.GetPermissionWithPagination;

namespace N5Permission.Api.Controllers
{ 
    public class WeatherForecastController : ApiControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

         

        [HttpGet(Name = "GetPermission")]
        public async Task<ActionResult<List<GetPermissionsQueryResponse>>> GetPermission()
        {
            var result = await Mediator.Send(new GetPermissionsQuery());
            return result;
        }
    }
}