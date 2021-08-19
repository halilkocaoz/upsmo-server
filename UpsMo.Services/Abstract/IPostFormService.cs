using System;
using System.Threading.Tasks;
using UpsMo.Common.DTO.Request.Monitor;
using UpsMo.Common.Response;

namespace UpsMo.Services.Abstract
{
    public interface IPostFormService
    {
        /// <summary>
        /// Creates post form data for monitor
        /// <para>Authenticated user must be founder or admin of the related organization</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.Created"/> and created object,</para> 
        /// <para><see cref="ResponseStatus.BadRequest"/>, <see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> CreateByRequestAsync(PostFormRequest request);

        /// <summary>
        /// Updates
        /// <para>Authenticated user must be founder or admin of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.OK"/> and updated object,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> UpdateByRequestAsync(PostFormRequest request);

        /// <summary>
        /// Soft deletes
        /// <para>Authenticated user must be founder or admin of the related organization</para> 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// <see cref="ApiResponse"/> with <para> <see cref="ResponseStatus.NoContent"/>,</para> 
        /// <para><see cref="ResponseStatus.NotFound"/> or <see cref="ResponseStatus.Forbid"/> </para>
        /// </returns>
        Task<ApiResponse> SoftDeleteByRequestAsync(PostFormRequest request);
    }
}