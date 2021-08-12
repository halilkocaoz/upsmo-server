using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;
using UpMo.Entities;

namespace UpMo.Services.Abstract
{
    public interface IOrganizationService
    {
        /// <summary>
        /// Create a new organization for authenticated user
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.Created"/> and created organization object.</returns>
        Task<ApiResponse> CreateAsync(OrganizationCreateRequest request);

        /// <summary>
        /// Update a organization by ID, if <see cref="OrganizationUpdateRequest.AuthenticatedUserID"/> equals any admin or creator user id for the Organization.
        /// </summary>
        /// <param name="toBeUpdatedOrganizationID"></param>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.OK"/> and updated organization object or <see cref="ResponseStatus.Forbid"/>.</returns>
        Task<ApiResponse> UpdateAsyncByID(Guid toBeUpdatedOrganizationID, OrganizationUpdateRequest request);
    }
}