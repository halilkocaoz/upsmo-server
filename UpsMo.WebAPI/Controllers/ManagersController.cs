using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpsMo.Common.DTO.Request.Organization;
using UpsMo.Services.Abstract;
using UpsMo.WebAPI.Controllers.Base;
using UpsMo.WebAPI.Extensions;

namespace UpsMo.WebAPI.Controllers
{
    [Route("organizations/{organizationID}")]
    public class ManagersController : AuthorizeController
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService) =>
            _managerService = managerService;

        [HttpGet("managers")]
        public async Task<IActionResult> GetManagersByOrganizationIDForAuthenticatedUserAsync(
            Guid organizationID) =>
            ApiResponse(await _managerService.GetManagersByOrganizationIDAndAuthenticatedUserID(organizationID, User.GetID()));

        [HttpPost("managers")]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            ManagerCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerService.CreateByRequestAsync(request));
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
            return ApiResponse(await _managerService.UpdateByRequestAsync(request));
        }

        [HttpDelete("managers/{managerID}")]
        public async Task<IActionResult> DeleteAsync(
            Guid organizationID,
            Guid managerID) =>
            ApiResponse(await _managerService.SoftDeleteByIDsAsync(organizationID, managerID, User.GetID()));
    }
}