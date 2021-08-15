using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IPostFormService
    {
        Task<ApiResponse> CreateByRequestAsync(PostFormRequest request);

        Task<ApiResponse> UpdateByRequestAsync(PostFormRequest request);

        Task<ApiResponse> SoftDeleteByIDsAsync(Guid postFormID, Guid monitorID, Guid organizationID, int authenticatedUserID);
    }
}