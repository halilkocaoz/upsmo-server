using System.Threading.Tasks;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IUserService
    {
        /// <summary>
        /// SigIn with Email or UserName and Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with status code 200 and bearer token or status code 400</returns>
        Task<ApiResponse> SignIn(SignInRequest request);

        /// <summary>
        /// SignUp with Email, UserName and Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with status code 200 and bearer token or status code 400 and errors</returns>
        Task<ApiResponse> SignUp(SignUpRequest request);
    }
}