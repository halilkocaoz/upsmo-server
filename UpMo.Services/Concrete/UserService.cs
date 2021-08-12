using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;
using UpMo.Entities;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(IMapper mapper, ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> SignIn(SignInRequest request)
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

            return new ApiResponse(ResponseStatus.BadRequest, "Invalid credentials");
        }

        public async Task<ApiResponse> SignUp(SignUpRequest request)
        {
            var newUser = _mapper.Map<AppUser>(request);
            var identityResult = await _userManager.CreateAsync(newUser, request.Password);
            if (identityResult.Succeeded)
            {
                return new ApiResponse(ResponseStatus.OK, new { token = _tokenService.CreateToken(newUser) });
            }

            return new ApiResponse(ResponseStatus.BadRequest, new { errors = identityResult.Errors });
        }
    }
}