using Microsoft.AspNetCore.Mvc;
using UpMo.Common.Response;

namespace UpMo.WebAPI.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult ApiResponse(ApiResponse response) => 
            response.StatusCode == 204 ? NoContent() : StatusCode(response.StatusCode, response);
    }
}