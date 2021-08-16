using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("/organizations/{organizationID}/monitors/{monitorID}/postforms")]
    public class PostFormsController : AuthorizeController
    {
        private readonly IPostFormService _postFormService;

        public PostFormsController(IPostFormService postFormService) =>
            _postFormService = postFormService;

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            Guid monitorID,
            PostFormRequest request)
        {
            request.OrganizationID = organizationID;
            request.MonitorID = monitorID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _postFormService.CreateByRequestAsync(request));
        }

        [HttpPut("{postFormID}")]
        public async Task<IActionResult> UpdateAsync(
            Guid organizationID,
            Guid monitorID,
            Guid postFormID,
            PostFormRequest request)
        {
            request.OrganizationID = organizationID;
            request.MonitorID = monitorID;
            request.ID = postFormID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _postFormService.UpdateByRequestAsync(request));
        }

        [HttpDelete("{postFormID}")]
        public async Task<IActionResult> DeleteAsync(
            Guid organizationID,
            Guid monitorID,
            Guid postFormID) =>
            ApiResponse(await _postFormService.SoftDeleteByRequestAsync(new PostFormRequest
            {
                ID = postFormID,
                OrganizationID = organizationID,
                MonitorID = monitorID,
                AuthenticatedUserID = User.GetID()
            }));
    }
}