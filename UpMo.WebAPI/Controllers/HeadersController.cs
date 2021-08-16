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
    }
}