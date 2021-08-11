using System.Threading.Tasks;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IOrganizationService
    {
        Task<ApiResponse> CreateAsync(OrganizationCreateRequest request);
    }
}