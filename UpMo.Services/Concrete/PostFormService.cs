using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.DTO.Response.Monitor;
using UpMo.Common.Monitor;
using UpMo.Common.Response;
using UpMo.Data;
using UpMo.Data.Extensions;
using UpMo.Entities;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class PostFormService : BaseService, IPostFormService
    {
        public PostFormService(
            IMapper mapper,
            UpMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(PostFormRequest request)
        {
            var toBeRelatedMonitor = await _context.Monitors.Include(x => x.Organization).ThenInclude(x => x.Managers)
                                                 .SingleOrDefaultAsync(x =>
                                                       x.ID == request.MonitorID
                                                    && x.OrganizationID == request.OrganizationID);

            if (toBeRelatedMonitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            bool userHasPermissionToCreatePostFormForMonitor = toBeRelatedMonitor.Organization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToCreatePostFormForMonitor is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }
            
            if (toBeRelatedMonitor.Method.ToUpper() is not "POST")
            {
                return new ApiResponse(ResponseStatus.BadRequest, ResponseMessage.MonitorNotPost);
            }

            var newPostForm = _mapper.Map<PostForm>(request);
            await _context.AddAsync(newPostForm);
            await _context.SaveChangesAsync();

            return new ApiResponse(ResponseStatus.Created, new { postForm = _mapper.Map<PostFormResponse>(newPostForm) });
        }

        private async Task<PostForm> getPostFormByRequestAsync(PostFormRequest request) =>
            await _context.PostForms.Include(x => x.Monitor)
                                    .ThenInclude(x => x.Organization)
                                    .ThenInclude(x => x.Managers)
                                    .SingleOrDefaultAsync(x => x.ID == request.ID
                                                               && x.MonitorID == request.MonitorID
                                                               && x.Monitor.OrganizationID == request.OrganizationID);

        public async Task<ApiResponse> UpdateByRequestAsync(PostFormRequest request)
        {
            var toBeUpdatedPostForm = await getPostFormByRequestAsync(request);
            if (toBeUpdatedPostForm is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitorPostForm);
            }

            bool userHasPermissionToUpdate = toBeUpdatedPostForm.Monitor.Organization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToUpdate is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            toBeUpdatedPostForm = _mapper.Map(request, toBeUpdatedPostForm);
            await _context.SaveChangesAsync();
            return new ApiResponse(ResponseStatus.OK, new { postForm = _mapper.Map<PostFormResponse>(toBeUpdatedPostForm) });
        }

        public async Task<ApiResponse> SoftDeleteByRequestAsync(PostFormRequest request)
        {
            var toBeSoftDeletedPostForm = await getPostFormByRequestAsync(request);
            if (toBeSoftDeletedPostForm is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitorPostForm);
            }

            bool userHasPermissionToSoftDelete = toBeSoftDeletedPostForm.Monitor.Organization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToSoftDelete is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }
            
            toBeSoftDeletedPostForm.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return new ApiResponse(ResponseStatus.NoContent);
        }
    }
}