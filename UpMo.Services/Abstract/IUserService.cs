using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Auth;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IUserService
    {
        /// <summary>
        /// SigIn with Email or UserName and Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and <see cref="UpMo.Common.DTO.Response.Token"/> or <see cref="ResponseStatus.BadRequest"/>
        /// </returns>
        Task<ApiResponse> SignInWithPasswordAsync(SignInRequest request);

        /// <summary>
        /// SignUp with Email, UserName and Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and <see cref="UpMo.Common.DTO.Response.Token"/> or <see cref="ResponseStatus.BadRequest"/>
        /// </returns>
        Task<ApiResponse> SignUpWithPasswordAsync(SignUpRequest request);

        Task<ApiResponse> SignInWithSocialAsync();

        Task<ApiResponse> SignUpWithSocialAsync();
    }
}