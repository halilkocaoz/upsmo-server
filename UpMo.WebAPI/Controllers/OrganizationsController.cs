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
        public async Task<IActionResult> GetOrganizationsForAuthenticatedUserAsync() =>
            ApiResponse(await _organizationService.GetOrganizationsByAuthenticatedUserIDAsync(User.GetID()));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrganizationCreateRequest request)
        {
            request.CreatorUserID = User.GetID();
            return ApiResponse(await _organizationService.CreateByRequestAsync(request));
        }

        [HttpPut("{organizationID}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid organizationID, [FromBody] OrganizationUpdateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _organizationService.UpdateByRequestAsync(request));
        }

        [HttpGet("{organizationID}/Managers")]
        public async Task<IActionResult> GetManagersByOrganizationIDForAuthenticatedUserAsync([FromRoute] Guid organizationID) => 
            ApiResponse(await _managerOrganizationService.GetManagersByOrganizationIDAndAuthenticatedUserID(organizationID, User.GetID()));

        [HttpPost("{organizationID}/Managers")]
        public async Task<IActionResult> CreateManagerAsync([FromRoute] Guid organizationID, [FromBody] OrganizationManagerCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.CreateByRequestAsync(request));
        }

        [HttpPut("{organizationID}/Managers/{organizationManagerID}")]
        public async Task<IActionResult> UpdateManagerAsync([FromRoute] Guid organizationID, [FromRoute] Guid organizationManagerID, [FromBody] OrganizationManagerUpdateRequest request)
        {
            request.OrganizationManagerID = organizationManagerID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _managerOrganizationService.UpdateByRequestAsync(request));
        }
    }
}