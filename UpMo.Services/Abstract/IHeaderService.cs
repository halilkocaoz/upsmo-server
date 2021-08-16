using System;
using System.Threading.Tasks;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.Response;

namespace UpMo.Services.Abstract
{
    public interface IHeaderService
    {
        /// <summary>
        /// Creates header for monitor
        /// <para>Authenticated user must be founder or admin of the related organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.Created"/> and created object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> CreateByRequestAsync(HeaderRequest request);

        /// <summary>
        /// Updates
        /// <para>Authenticated user must be founder or admin of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.OK"/> and updated object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> UpdateByRequestAsync(HeaderRequest request);

        /// <summary>
        /// Soft deletes
        /// <para>Authenticated user must be founder or admin of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.NoContent"/>,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> SoftDeleteByRequestAsync(HeaderRequest request);
    }
}