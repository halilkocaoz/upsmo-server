using Microsoft.AspNetCore.Authorization;

namespace UpMo.WebAPI.Controllers.Base
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthorizeController : BaseController { }
}