using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationManagerService _managerOrganizationService;

        public OrganizationsController(IOrganizationService organizationService, IOrganizationManagerService managerOrganizationService)
        {
            _organizationService = organizationService;
            _managerOrganizationService = managerOrganizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizationsForAuthenticatedUser()
        {
            return ApiResponse(await _organizationService.GetOrganizationsByAuthenticatedUserIDAsync(User.GetId()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrganizationCreateRequest request)
        {
            request.CreatorUserID = User.GetId();
            return ApiResponse(await _organizationService.CreateAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] OrganizationUpdateRequest request)
        {
            request.AuthenticatedUserID = User.GetId();
            return ApiResponse(await _organizationService.UpdateAsyncByID(toBeUpdatedOrganizationID: id, request));
        }


        [HttpPost("{id}/Managers")]
        public async Task<IActionResult> CreateManagerForOrganizationAsync([FromRoute] Guid id, [FromBody] OrganizationManagerCreateRequest request)
        {
            request.OrganizationID = id;
            request.AuthenticatedUserID = User.GetId();
            return ApiResponse(await _managerOrganizationService.CreateAsync(request));
        }
    }
}