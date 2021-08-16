using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IMonitorService
    {
        /// <summary>
        /// Creates monitor for organization
        /// <para>Authenticated user must be founder or admin of the related organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.Created"/> and created object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> CreateByRequestAsync(MonitorCreateRequest request);

        /// <summary>
        /// Updates
        /// <para>Authenticated user must be founder or admin of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.OK"/> and updated object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> UpdateByRequestAsync(MonitorUpdateRequest request);

        Task<ApiResponse> SoftDeleteByIDsAsync(Guid organizationID, Guid monitorID, int authenticatedUserID);

        /// <summary>
        /// Gets monitors
        /// <para>Authenticated user must be founder, admin or viewer of the related organization</para>
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="authenticatedUserID"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/></returns>
        Task<ApiResponse> GetMonitorsByOrganizationIDForAuthenticatedUser(Guid organizationID, int authenticatedUserID);
    }
}