using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UpsMo.Common.DTO.Request.Auth;
using UpsMo.Common.Response;
using UpsMo.Data;
using UpsMo.Entities;
using UpsMo.Services.Abstract;

namespace UpsMo.Services.Concrete
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(IMapper mapper,
                           UpsMoContext context,
                           ILogger<UserService> logger,
                           UserManager<AppUser> userManager,
                           ITokenService tokenService) : base(mapper, context, logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> SignInWithPasswordAsync(SignInRequest request)
        {
            bool isIdentifierEmail = request.Identifier.Contains("@");
            var user = isIdentifierEmail ? await _userManager.FindByEmailAsync(request.Identifier) : await _userManager.FindByNameAsync(request.Identifier);

            if (user != null)
            {
                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isPasswordValid)
                {
                    return new ApiResponse(ResponseStatus.OK, new { token = _tokenService.CreateToken(user) });
                }
            }

            return new ApiResponse(ResponseStatus.BadRequest, ResponseMessage.InvalidCredentials);
        }

        public async Task<ApiResponse> SignUpWithPasswordAsync(SignUpRequest request)
        {
            var newUser = _mapper.Map<AppUser>(request);
            var identityResult = await _userManager.CreateAsync(newUser, request.Password);
            if (identityResult.Succeeded)
            {
                return new ApiResponse(ResponseStatus.OK, new { token = _tokenService.CreateToken(newUser) });
            }

            return new ApiResponse(ResponseStatus.BadRequest, new { errors = identityResult.Errors });
        }

        public Task<ApiResponse> SignInWithSocialAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> SignUpWithSocialAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}