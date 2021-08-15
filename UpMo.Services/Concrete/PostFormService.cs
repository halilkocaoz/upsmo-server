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

        public async Task<ApiResponse> CreateByRequestAsync(PostFormCreateRequest request)
        {
            var monitor = await _context.Monitors.Include(x => x.Organization)
                                                 .ThenInclude(x => x.Managers)
                                                 .SingleOrDefaultAsync(x =>
                                                    x.ID == request.MonitorID
                                                    && x.OrganizationID == request.OrganizationID);

            if (monitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            if (monitor.Organization.CheckCreatorOrAdmin(request.AuthenticatedUserID))
            {
                if (monitor.Method is not MonitorMethodType.POST)
                {
                    return new ApiResponse(ResponseStatus.BadRequest, ResponseMessage.MonitorNotPost);
                }

                var newPostForm = _mapper.Map<PostForm>(request);
                await _context.AddAsync(newPostForm);
                await _context.SaveChangesAsync();

                return new ApiResponse(ResponseStatus.Created, new { postForm = _mapper.Map<PostFormResponse>(newPostForm) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> UpdateByRequestAsync(PostFormUpdateRequest request)
        {
            var toBeUpdatedPostForm = await _context.PostForms.Include(x => x.Monitor)
                                                                 .ThenInclude(x => x.Organization)
                                                                 .ThenInclude(x => x.Managers)
                                                                 .SingleOrDefaultAsync(x =>
                                                                    x.ID == request.ID &&
                                                                    x.MonitorID == request.MonitorID &&
                                                                    x.Monitor.OrganizationID == request.OrganizationID);

            if (toBeUpdatedPostForm is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitorPostForm);
            }

            if (toBeUpdatedPostForm.Monitor.Organization.CheckCreatorOrAdmin(request.AuthenticatedUserID))
            {
                toBeUpdatedPostForm = _mapper.Map(request, toBeUpdatedPostForm);
                await _context.SaveChangesAsync();
                return new ApiResponse(ResponseStatus.OK, new { postForm = _mapper.Map<PostFormResponse>(toBeUpdatedPostForm) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> SoftDeleteByIDsAsync(Guid postFormID, Guid monitorID, Guid organizationID, int authenticatedUserID)
        {
            var toBeSoftDeletedPostForm = await _context.PostForms.Include(x => x.Monitor)
                                                                 .ThenInclude(x => x.Organization)
                                                                 .ThenInclude(x => x.Managers)
                                                                 .SingleOrDefaultAsync(x =>
                                                                    x.ID == postFormID &&
                                                                    x.MonitorID == monitorID &&
                                                                    x.Monitor.OrganizationID == organizationID);
            if (toBeSoftDeletedPostForm is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitorPostForm);
            }

            if (toBeSoftDeletedPostForm.Monitor.Organization.CheckCreatorOrAdmin(authenticatedUserID))
            {
                toBeSoftDeletedPostForm.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return new ApiResponse(ResponseStatus.NoContent);
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }
    }
}