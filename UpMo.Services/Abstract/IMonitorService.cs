using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IMonitorService
    {
        /// <summary>
        /// Creates monitor for Organization
        /// <para>Authenticated user must be creator or admin of the Organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.Created"/> and created monitor object, <see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/></returns>
        Task<ApiResponse> CreateByRequestAsync(MonitorCreateRequest request);

        /// <summary>
        /// Updates monitor for Organization
        /// <para>Authenticated user must be creator or admin of the Organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and updated monitor object, <see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/></returns>
        Task<ApiResponse> UpdateByRequestAsync(MonitorUpdateRequest request);

        Task<ApiResponse> SoftDeleteByIDsAsync(Guid organizationID, Guid monitorID, int authenticatedUserID);

        /// <summary>
        /// Gets monitors by Organization ID
        /// <para>Authenticated user must be creator, admin or viewer of the Organization</para>
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="authenticatedUserID"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/></returns>
        Task<ApiResponse> GetMonitorsByOrganizationIDForAuthenticatedUser(Guid organizationID, int authenticatedUserID);
    }
}