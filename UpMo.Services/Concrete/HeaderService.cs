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
    public class HeaderService : BaseService, IHeaderService
    {
        public HeaderService(
            IMapper mapper,
            UpMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(HeaderRequest request)
        {
            var toBeRelatedMonitor = await _context.Monitors.Include(x => x.Organization).ThenInclude(x => x.Managers)
                                                            .SingleOrDefaultAsync(x => x.ID == request.MonitorID
                                                                                       && x.OrganizationID == request.OrganizationID);

            if (toBeRelatedMonitor is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundMonitor);
            }

            bool userHasPermissionToCreateHeaderForMonitor = toBeRelatedMonitor.Organization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToCreateHeaderForMonitor is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            var newHeader = _mapper.Map<Header>(request);
            await _context.AddAsync(newHeader);
            await _context.SaveChangesAsync();

            return new ApiResponse(ResponseStatus.Created, new { header = _mapper.Map<HeaderResponse>(newHeader) });
        }

        public Task<ApiResponse> SoftDeleteByRequestAsync(HeaderRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateByRequestAsync(HeaderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}