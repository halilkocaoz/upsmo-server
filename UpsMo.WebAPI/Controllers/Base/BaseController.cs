using Microsoft.AspNetCore.Mvc;
using UpsMo.Common.Response;

namespace UpsMo.WebAPI.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult ApiResponse(ApiResponse response) => 
            response.StatusCode == 204 ? NoContent() : StatusCode(response.StatusCode, response);
    }
}