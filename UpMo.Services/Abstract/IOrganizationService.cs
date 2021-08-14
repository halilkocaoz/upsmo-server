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
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.Created"/> and created organization object.</returns>
        Task<ApiResponse> CreateByRequestAsync(OrganizationCreateRequest request);

        /// <summary>
        /// Updates organization by ID, if <see cref="OrganizationUpdateRequest.AuthenticatedUserID"/> equals any admin or creator user id for the Organization.
        /// <para>Authenticated user must be creator or admin of the Organization</para>
        /// </summary>
        /// <param name="toBeUpdatedOrganizationID"></param>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and updated organization object or <see cref="ResponseStatus.Forbid"/>.</returns>
        Task<ApiResponse> UpdateByRequestAsync(OrganizationUpdateRequest request);

        /// <summary>
        /// Gets organizations for authenticated user by id of user.
        /// </summary>
        /// <param name="authenticatedUserID"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and a list of organizations</returns>
        Task<ApiResponse> GetOrganizationsByAuthenticatedUserIDAsync(int authenticatedUserID);
    }
}