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
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and bearer token or <see cref="ResponseStatus.BadRequest"/></returns>
        Task<ApiResponse> SignInWithPasswordAsync(SignInRequest request);

        /// <summary>
        /// SignUp with Email, UserName and Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and bearer token or <see cref="ResponseStatus.BadRequest"/> and errors</returns>
        Task<ApiResponse> SignUpWithPasswordAsync(SignUpRequest request);

        /// <summary>
        /// SigIn with Social Providers
        /// </summary>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and bearer token or <see cref="ResponseStatus.BadRequest"/></returns>
        Task<ApiResponse> SignInWithSocialAsync();

        /// <summary>
        /// SigUp with Social Providers
        /// </summary>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and bearer token or <see cref="ResponseStatus.BadRequest"/> and errors</returns>
        Task<ApiResponse> SignUpWithSocialAsync();
    }
}