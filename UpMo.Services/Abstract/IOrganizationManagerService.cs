using System.Threading.Tasks;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IOrganizationManagerService
    {
        /// <summary>
        /// Creates a manager for organization by <see cref="OrganizationManagerCreateRequest"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.Created"/>, <see cref="ResponseStatus.BadRequest"/>, <see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/></returns>
        Task<ApiResponse> CreateAsync(OrganizationManagerCreateRequest request);
    }
}