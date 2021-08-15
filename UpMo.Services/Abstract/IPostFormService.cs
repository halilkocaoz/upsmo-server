using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IPostFormService
    {
        Task<ApiResponse> CreateByRequestAsync(PostFormCreateRequest request);

        Task<ApiResponse> UpdateByRequestAsync(PostFormUpdateRequest request);

        Task<ApiResponse> SoftDeleteByIDsAsync(Guid postFormID, Guid monitorID, Guid organizationID, int authenticatedUserID);
    }
}