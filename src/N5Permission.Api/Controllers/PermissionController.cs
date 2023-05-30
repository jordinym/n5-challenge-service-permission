using Microsoft.AspNetCore.Mvc;
using N5Permission.Application.Permission.Commands.CreatePermission;
using N5Permission.Application.Permission.Commands.CreatePermissionType;
using N5Permission.Application.Permission.Commands.UpdatePermission;
using N5Permission.Application.Permission.Commands.UpdatePermissionType;
using N5Permission.Application.Permission.Queries.GetPermissions;
using N5Permission.Application.Permission.Queries.GetPermissionTypes;
using N5Permission.Application.Permission.Queries.GetPermissionWithPagination;

namespace N5Permission.Api.Controllers
{
    public class PermissionController : ApiControllerBase
    {
        private readonly ILogger<PermissionController> _logger;

        public PermissionController(ILogger<PermissionController> logger)
        {
            _logger = logger;
        }
         
        [HttpGet]
        public async Task<ActionResult<List<GetPermissionsQueryResponse>>> GetPermissions()
        {
            _logger.LogInformation("This is a wow message to test the logs");
            var result = await Mediator.Send(new GetPermissionsQuery());
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<int>> SavePermission(CreatePermissionCommand createPermissionCommand)
        {
            var result = await Mediator.Send(createPermissionCommand);
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdatePermission(UpdatePermissionCommand updatePermissionCommand)
        {
            var result = await Mediator.Send(updatePermissionCommand);
            return result;
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<GetPermissionTypesQueryResponse>>> GetPermissionTypes()
        {
            _logger.LogInformation("This is a wow message to test the logs");
            var result = await Mediator.Send(new GetPermissionTypesQuery());
            return result;
        }
         
        [HttpPost("types")]
        public async Task<ActionResult<int>> SavePermissionType(CreatePermissionTypeCommand createPermissionTypeCommand)
        {
            var result = await Mediator.Send(createPermissionTypeCommand);
            return result;
        }

        [HttpPut("types")]
        public async Task<ActionResult<int>> UpdatePermissionType(UpdatePermissionTypeCommand updatePermissionTypeCommand)
        {
            var result = await Mediator.Send(updatePermissionTypeCommand);
            return result;
        }
    }
}
