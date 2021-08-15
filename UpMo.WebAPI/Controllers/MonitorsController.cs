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
        public async Task<IActionResult> GetMonitorsByOrganizationIDForAuthenticatedUserAsync(Guid organizationID) =>
            ApiResponse(await _monitorService.GetMonitorsByOrganizationIDForAuthenticatedUser(organizationID, User.GetID()));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            MonitorCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.CreateByRequestAsync(request));
        }

        [HttpPut("{monitorID}")]
        public async Task<IActionResult> UpdateAsync(
            Guid organizationID,
            Guid monitorID,
            MonitorUpdateRequest request)
        {
            request.ID = monitorID;
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _monitorService.UpdateByRequestAsync(request));
        }

        [HttpDelete("{monitorID}")]
        public async Task<IActionResult> DeleteAsync(
            Guid organizationID,
            Guid monitorID) =>
            ApiResponse(await _monitorService.SoftDeleteByIDsAsync(organizationID, monitorID, User.GetID()));
    }
}