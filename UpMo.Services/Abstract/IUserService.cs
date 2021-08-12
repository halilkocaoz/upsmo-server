using System.Threading.Tasks;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IUserService
    {
        Task<ApiResponse> SignIn(SignInRequest request);
        Task<ApiResponse> SignUp(SignUpRequest request);
    }
}