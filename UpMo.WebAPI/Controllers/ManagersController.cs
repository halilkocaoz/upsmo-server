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
        public async Task<IActionResult> GetManagersByOrganizationIDForAuthenticatedUserAsync(
            Guid organizationID) =>
            ApiResponse(await _managerOrganizationService.GetManagersByOrganizationIDAndAuthenticatedUserID(organizationID, User.GetID()));

        [HttpPost("managers")]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            ManagerCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.CreateByRequestAsync(request));
        }

        [HttpPut("managers/{managerID}")]
        public async Task<IActionResult> UpdateAsync(
            Guid organizationID,
            Guid managerID,
            ManagerUpdateRequest request)
        {
            request.ID = managerID;
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.UpdateByRequestAsync(request));
        }

        [HttpDelete("managers/{managerID}")]
        public async Task<IActionResult> DeleteAsync(
            Guid organizationID,
            Guid managerID) =>
            ApiResponse(await _managerOrganizationService.SoftDeleteByIDsAsync(organizationID, managerID, User.GetID()));
    }
}