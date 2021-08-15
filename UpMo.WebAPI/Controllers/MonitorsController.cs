using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("organizations/{organizationID}/monitors")]
    public class MonitorsController : AuthorizeController
    {
        private readonly IMonitorService _monitorService;

        public MonitorsController(IMonitorService monitorService) =>
            _monitorService = monitorService;

        [HttpGet]
        public async Task<IActionResult> GetMonitorsByOrganizationIDForAuthenticatedUserAsync([FromRoute] Guid organizationID) =>
            ApiResponse(await _monitorService.GetMonitorsByOrganizationIDForAuthenticatedUser(organizationID, User.GetID()));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromRoute] Guid organizationID,
            [FromBody] MonitorCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.CreateByRequestAsync(request));
        }

        [HttpPut("{monitorID}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid organizationID,
            [FromRoute] Guid monitorID,
            [FromBody] MonitorUpdateRequest request)
        {
            request.ID = monitorID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.UpdateByRequestAsync(request));
        }

        [HttpDelete("{monitorID}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid organizationID,
            [FromRoute] Guid monitorID) =>
            ApiResponse(await _monitorService.SoftDeleteByIDAsync(monitorID: monitorID, User.GetID()));
    }
}