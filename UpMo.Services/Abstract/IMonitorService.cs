using System.Threading.Tasks;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IMonitorService
    {
        /// <summary>
        /// Create monitor for Organization
        /// <para>Authenticated user must be creator or admin of the Organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns><see cref="ApiResponse"/> with <see cref="ResponseStatus.Created"/> and created organization object, <see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/></returns>
        Task<ApiResponse> CreateByRequestAsync(MonitorCreateRequest request);
    }
}