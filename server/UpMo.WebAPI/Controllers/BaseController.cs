using Microsoft.AspNetCore.Mvc;
using UpMo.Common.Response;

namespace UpMo.WebAPI.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult ApiResponse(ApiResponse response) => StatusCode(response.StatusCode, response);
    }
}