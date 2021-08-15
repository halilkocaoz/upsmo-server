using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("organizations/{organizationID}")]
    public class ManagersController : AuthorizeController
    {
        private readonly IOrganizationManagerService _managerOrganizationService;

        public ManagersController(IOrganizationManagerService managerOrganizationService) =>
            _managerOrganizationService = managerOrganizationService;

        [HttpGet("managers")]
        public async Task<IActionResult> GetManagersByOrganizationIDForAuthenticatedUserAsync([FromRoute] Guid organizationID) =>
            ApiResponse(await _managerOrganizationService.GetManagersByOrganizationIDAndAuthenticatedUserID(organizationID, User.GetID()));

        [HttpPost("managers")]
        public async Task<IActionResult> CreateAsync(
            [FromRoute] Guid organizationID,
            [FromBody] OrganizationManagerCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.CreateByRequestAsync(request));
        }

        [HttpPut("managers/{managerID}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] Guid organizationID,
            [FromRoute] Guid managerID,
            [FromBody] OrganizationManagerUpdateRequest request)
        {
            request.ID = managerID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.UpdateByRequestAsync(request));
        }

        [HttpDelete("managers/{managerID}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid organizationID,
            [FromRoute] Guid managerID) =>
            ApiResponse(await _managerOrganizationService.SoftDeleteByIDAsync(organizationManagerID: managerID, User.GetID()));
    }
}