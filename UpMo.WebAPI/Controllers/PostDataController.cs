using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("/organizations/{organizationID}/monitors/{monitorID}/postdata")]
    public class PostDataController : AuthorizeController
    {
        private readonly IMonitorPostDataService _postDataService;

        public PostDataController(IMonitorPostDataService postDataService) =>
            _postDataService = postDataService;

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            Guid monitorID,
            PostFormDataCreateRequest request)
        {
            request.OrganizationID = organizationID;
            request.MonitorID = monitorID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _postDataService.CreateByRequestAsync(request));
        }

        [HttpPut("{postDataID}")]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            Guid monitorID,
            Guid postDataID,
            PostFormDataUpdateRequest request)
        {
            request.OrganizationID = organizationID;
            request.MonitorID = monitorID;
            request.ID = postDataID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _postDataService.UpdateByRequestAsync(request));
        }
    }
}