using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpMo.Common.DTO.Request.Auth;
using UpMo.Services.Abstract;
using UpMo.WebAPI.Controllers.Base;

namespace UpMo.WebAPI.Controllers
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) =>
            _userService = userService;

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInRequest request) =>
            ApiResponse(await _userService.SignInWithPasswordAsync(request));

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequest request) =>
            ApiResponse(await _userService.SignUpWithPasswordAsync(request));
    }
}