using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("Organizations/{organizationID}")]
    public class ManagersController : AuthorizeController
    {
        private readonly IOrganizationManagerService _managerOrganizationService;

        public ManagersController(IOrganizationManagerService managerOrganizationService) =>
            _managerOrganizationService = managerOrganizationService;

        [HttpGet("Managers")]
        public async Task<IActionResult> GetManagersByOrganizationIDForAuthenticatedUserAsync([FromRoute] Guid organizationID) =>
            ApiResponse(await _managerOrganizationService.GetManagersByOrganizationIDAndAuthenticatedUserID(organizationID, User.GetID()));

        [HttpPost("Managers")]
        public async Task<IActionResult> CreateAsync([FromRoute] Guid organizationID, [FromBody] OrganizationManagerCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.CreateByRequestAsync(request));
        }

        [HttpPut("Managers/{organizationManagerID}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid organizationID, [FromRoute] Guid organizationManagerID, [FromBody] OrganizationManagerUpdateRequest request)
        {
            request.OrganizationManagerID = organizationManagerID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.UpdateByRequestAsync(request));
        }

        [HttpDelete("Managers/{organizationManagerID}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid organizationID, [FromRoute] Guid organizationManagerID) =>
            ApiResponse(await _managerOrganizationService.SoftDeleteByIDAsync(organizationManagerID: organizationManagerID, User.GetID()));
    }
}