using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IOrganizationService
    {
        /// <summary>
        /// Creates organization for authenticated user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <see cref="ResponseStatus.Created"/> and created object
        /// </returns>
        Task<ApiResponse> CreateByRequestAsync(OrganizationRequest request);

        /// <summary>
        /// Updates
        /// <para>Authenticated user must be founder or admin of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.OK"/> and updated object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> UpdateByRequestAsync(OrganizationRequest request);

        /// <summary>
        /// Soft deletes
        /// <para>Authenticated user must be founder of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.NoContent"/>,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> SoftDeleteByIDAsync(Guid organizationID, int authenticatedUserID);

        /// <summary>
        /// Gets organizations for authenticated user by id of user.
        /// </summary>
        /// <param name="authenticatedUserID"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/></returns>
        Task<ApiResponse> GetOrganizationsByAuthenticatedUserIDAsync(int authenticatedUserID);
    }
}