using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IMonitorPostDataService
    {
        Task<ApiResponse> CreateByRequestAsync(PostFormDataCreateRequest request);

        Task<ApiResponse> UpdateByRequestAsync(PostFormDataUpdateRequest request);

        Task<ApiResponse> SoftDeleteByIDsAsync(Guid postDataID, Guid monitorID, Guid organizationID, int authenticatedUserID);
    }
}