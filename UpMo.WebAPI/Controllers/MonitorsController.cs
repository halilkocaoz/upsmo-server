using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("Organizations/{organizationID}")]
    public class MonitorsController : AuthorizeController
    {
        private readonly IMonitorService _monitorService;

        public MonitorsController(IMonitorService monitorService)
            => _monitorService = monitorService;

        [HttpPost("Monitors")]
        public async Task<IActionResult> CreateMonitorAsync([FromRoute] Guid organizationID, [FromBody] MonitorCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.CreateByRequestAsync(request));
        }

        [HttpPut("Monitors/{organizationMonitorID}")]
        public async Task<IActionResult> UpdateMonitorAsync([FromRoute] Guid organizationID, [FromRoute] Guid organizationMonitorID, [FromBody] MonitorUpdateRequest request)
        {
            request.MonitorID = organizationMonitorID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.UpdateByRequestAsync(request));
        }
    }
}