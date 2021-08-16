using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IManagerService
    {
        /// <summary>
        /// Creates manager for organization.
        /// <para>Authenticated user must be founder of the related organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.Created"/> and created object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> CreateByRequestAsync(ManagerCreateRequest request);

        /// <summary>
        /// Updates
        /// <para>Authenticated user must be founder of the related organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.OK"/> and updated object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> UpdateByRequestAsync(ManagerUpdateRequest request);

        /// <summary>
        /// Soft deletes
        /// <para>Authenticated user must be founder of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.NoContent"/>,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> SoftDeleteByIDsAsync(Guid organizationID, Guid managerID, int authenticatedUserID);

        /// <summary>
        /// Get managers by Organization ID and Authenticated User ID
        /// <para>Authenticated user must be founder of the related organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/></returns>
        Task<ApiResponse> GetManagersByOrganizationIDAndAuthenticatedUserID(Guid organizationID, int authenticatedUserID);
    }
}