using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;
using UpMo.WebAPI.Extensions;

namespace UpMo.WebAPI.Controllers
{
    [Route("/organizations/{organizationID}/monitors/{monitorID}/headers")]
    public class HeadersController : AuthorizeController
    {
        private readonly IHeaderService _headerService;

        public HeadersController(IHeaderService headerService) =>
            _headerService = headerService;

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            Guid organizationID,
            Guid monitorID,
            HeaderRequest request)
        {
            request.OrganizationID = organizationID;
            request.MonitorID = monitorID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _headerService.CreateByRequestAsync(request));
        }

        [HttpPut("{headerID}")]
        public async Task<IActionResult> UpdateAsync(
            Guid organizationID,
            Guid monitorID,
            Guid headerID,
            HeaderRequest request)
        {
            request.OrganizationID = organizationID;
            request.MonitorID = monitorID;
            request.ID = headerID;
            request.AuthenticatedUserID = User.GetID();
            return ApiResponse(await _headerService.UpdateByRequestAsync(request));
        }

        [HttpDelete("{headerID}")]
        public async Task<IActionResult> DeleteAsync(
            Guid organizationID,
            Guid monitorID,
            Guid headerID) =>
            ApiResponse(await _headerService.SoftDeleteByRequestAsync(new HeaderRequest
            {
                OrganizationID = organizationID,
                MonitorID = monitorID,
                ID = headerID,
                AuthenticatedUserID = User.GetID()
            }));
    }
}