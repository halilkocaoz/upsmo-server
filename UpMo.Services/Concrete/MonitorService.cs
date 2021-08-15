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
            var toBeCreateMonitorOrganization = await _context.Organizations.Include(x => x.Managers)
                                                                            .SingleOrDefaultAsync(x => x.ID == request.OrganizationID);
            if (toBeCreateMonitorOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            if (toBeCreateMonitorOrganization.CheckCreatorOrAdmin(request.AuthenticatedUserID))
            {
                var newMonitor = _mapper.Map<Monitor>(request);

                await _context.AddAsync(newMonitor);
                await _context.SaveChangesAsync();

                return new ApiResponse(ResponseStatus.Created, new { monitor = _mapper.Map<MonitorResponse>(newMonitor) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> UpdateByRequestAsync(MonitorUpdateRequest request)
        {
            var toBeUpdatedMonitor = await _context.Monitors.Include(x => x.Organization)
                                                            .ThenInclude(x => x.Managers)
                                                            .SingleOrDefaultAsync(x => x.ID == request.MonitorID);
            if (toBeUpdatedMonitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            if (toBeUpdatedMonitor.Organization.CheckCreatorOrAdmin(request.AuthenticatedUserID))
            {
                toBeUpdatedMonitor = _mapper.Map(request, toBeUpdatedMonitor);
                await _context.SaveChangesAsync();

                return new ApiResponse(ResponseStatus.OK, new { monitor = _mapper.Map<MonitorResponse>(toBeUpdatedMonitor) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> GetMonitorsByOrganizationIDForAuthenticatedUser(Guid organizationID, int authenticatedUserID)
        {
            var monitorsForAuthenticatedUser = await _context.Monitors.Include(x => x.PostFormData)
                                                    .Include(x => x.Organization).ThenInclude(x => x.Managers)
                                                    .AsSplitQuery()
                                                    .Where(x => x.OrganizationID == organizationID
                                                                && (
                                                                    x.Organization.CreatorUserID == authenticatedUserID ||
                                                                    x.Organization.Managers.Any(x => (x.Viewer || x.Admin) && x.UserID == authenticatedUserID)
                                                                ))
                                                    .ToListAsync();

            object returnObject = new { monitors = _mapper.Map<List<MonitorResponse>>(monitorsForAuthenticatedUser) };
            return new ApiResponse(ResponseStatus.OK, returnObject);
        }

        public async Task<ApiResponse> SoftDeleteByIDAsync(Guid monitorID, int authenticatedUserID)
        {
            var toBeSoftDeletedMonitor = await _context.Monitors.Include(x => x.Organization)
                                                                .ThenInclude(x => x.Managers)
                                                                .SingleOrDefaultAsync(monitor => monitor.ID == monitorID);
            if (toBeSoftDeletedMonitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            if (toBeSoftDeletedMonitor.Organization.CheckCreatorOrAdmin(authenticatedUserID))
            {
                toBeSoftDeletedMonitor.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return new ApiResponse(ResponseStatus.NoContent);
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }
    }
}