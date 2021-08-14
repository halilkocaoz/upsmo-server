using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UpMo.Common.DTO.Request;
using UpMo.Common.DTO.Response;
using UpMo.Common.Monitor;
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
            var toBeCreateMonitorOrganization = await _context.Organizations.SingleOrDefaultAsync(x => x.ID == request.OrganizationID);
            if (toBeCreateMonitorOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            if (toBeCreateMonitorOrganization.CheckCreatorOrAdmin(request.AuthenticatedUserID))
            {
                var newMonitor = _mapper.Map<Monitor>(request);

                if (newMonitor.Method == MonitorMethodType.GET)
                { // todo: ignore too from mapping
                    newMonitor.PostFormData = null;
                }

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    await _context.AddAsync(newMonitor);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                return new ApiResponse(ResponseStatus.Created, _mapper.Map<MonitorResponse>(newMonitor));
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
                
                return new ApiResponse(ResponseStatus.OK, _mapper.Map<MonitorResponse>(toBeUpdatedMonitor));
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }
    }
}