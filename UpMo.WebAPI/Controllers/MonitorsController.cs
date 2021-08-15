using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("Organizations/{organizationID}")]
    public class MonitorsController : AuthorizeController
    {
        private readonly IMonitorService _monitorService;

        public MonitorsController(IMonitorService monitorService) =>
            _monitorService = monitorService;

        [HttpGet("Monitors")]
        public async Task<IActionResult> GetMonitorsByOrganizationIDForAuthenticatedUserAsync([FromRoute] Guid organizationID) =>
            ApiResponse(await _monitorService.GetMonitorsByOrganizationIDForAuthenticatedUser(organizationID, User.GetID()));

        [HttpPost("Monitors")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid organizationID, [FromBody] MonitorCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.CreateByRequestAsync(request));
        }

        [HttpPut("Monitors/{organizationMonitorID}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid organizationID, [FromRoute] Guid organizationMonitorID, [FromBody] MonitorUpdateRequest request)
        {
            request.ID = organizationMonitorID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.UpdateByRequestAsync(request));
        }

        [HttpDelete("Monitors/{organizationMonitorID}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid organizationID, [FromRoute] Guid organizationMonitorID) =>
            ApiResponse(await _monitorService.SoftDeleteByIDAsync(monitorID: organizationMonitorID, User.GetID()));
    }
}