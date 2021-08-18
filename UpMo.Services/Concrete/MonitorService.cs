using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UpMo.Common.DTO.Request.Monitor;
using UpMo.Common.DTO.Response.Monitor;
using UpMo.Common.Response;
using UpMo.Data;
using UpMo.Data.Extensions;
using UpMo.Entities;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class MonitorService : BaseService, IMonitorService
    {
        public MonitorService(IMapper mapper, UpMoContext context) : base(mapper, context) { }

        public async Task<ApiResponse> CreateByRequestAsync(MonitorCreateRequest request)
        {
            var toBeRelatedOrganization = await _context.Organizations.Include(x => x.Managers)
                                                                      .SingleOrDefaultAsync(x => x.ID == request.OrganizationID);
            if (toBeRelatedOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            bool userHasPermissionToCreateMonitorForOrganization = toBeRelatedOrganization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToCreateMonitorForOrganization is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            var newMonitor = _mapper.Map<Monitor>(request);

            await _context.AddAsync(newMonitor);
            await _context.SaveChangesAsync();

            return new ApiResponse(ResponseStatus.Created, new { monitor = _mapper.Map<MonitorResponse>(newMonitor) });
        }
        
        private async Task<Monitor> getMonitorByIDsAsync(Guid monitorID, Guid organizationID) =>
            await _context.Monitors.Include(x => x.Organization)
                                   .ThenInclude(x => x.Managers)
                                   .SingleOrDefaultAsync(x => x.ID == monitorID
                                                              && x.OrganizationID == organizationID);

        public async Task<ApiResponse> UpdateByRequestAsync(MonitorUpdateRequest request)
        {
            var toBeUpdatedMonitor = await getMonitorByIDsAsync(request.ID, request.OrganizationID);
            if (toBeUpdatedMonitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            bool userHasPermissionToUpdate = toBeUpdatedMonitor.Organization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToUpdate is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            toBeUpdatedMonitor = _mapper.Map(request, toBeUpdatedMonitor);
            await _context.SaveChangesAsync();

            return new ApiResponse(ResponseStatus.OK, new { monitor = _mapper.Map<MonitorResponse>(toBeUpdatedMonitor) });
        }

        public async Task<ApiResponse> SoftDeleteByIDsAsync(Guid organizationID, Guid monitorID, int authenticatedUserID)
        {
            var toBeSoftDeletedMonitor = await getMonitorByIDsAsync(monitorID, organizationID);
            if (toBeSoftDeletedMonitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            bool userHasPermissionToSoftDelete = toBeSoftDeletedMonitor.Organization.CheckFounderOrAdmin(authenticatedUserID);
            if (userHasPermissionToSoftDelete is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            toBeSoftDeletedMonitor.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new ApiResponse(ResponseStatus.NoContent);
        }

        public async Task<ApiResponse> GetMonitorsByOrganizationIDForAuthenticatedUser(Guid organizationID, int authenticatedUserID)
        {
            var monitorsForAuthenticatedUser = await _context.Monitors.Include(x => x.PostForms).Include(x => x.Headers)
                                                    .Include(x => x.Organization).ThenInclude(x => x.Managers)
                                                    .AsSplitQuery()
                                                    .Where(x => x.OrganizationID == organizationID
                                                                && (x.Organization.FounderUserID == authenticatedUserID
                                                                    || x.Organization.Managers.Any(x => (x.Viewer || x.Admin)
                                                                    && x.UserID == authenticatedUserID)
                                                                )).ToListAsync();

            object returnObject = new { monitors = _mapper.Map<List<MonitorResponse>>(monitorsForAuthenticatedUser) };
            return new ApiResponse(ResponseStatus.OK, returnObject);
        }
    }
}