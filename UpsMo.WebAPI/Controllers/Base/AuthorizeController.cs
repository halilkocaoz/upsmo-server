using Microsoft.AspNetCore.Authorization;

namespace UpsMo.WebAPI.Controllers.Base
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthorizeController : BaseController { }
}