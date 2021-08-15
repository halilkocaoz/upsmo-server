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
    public class MonitorPostDataService : BaseService, IMonitorPostDataService
    {
        public MonitorPostDataService(
            IMapper mapper,
            UpMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(PostFormDataCreateRequest request)
        {
            var monitor = await _context.Monitors.Include(x => x.Organization).ThenInclude(x => x.Managers)
                                                 .SingleOrDefaultAsync(x => x.ID == request.MonitorID);

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

                var newPostData = _mapper.Map<PostFormData>(request);
                await _context.AddAsync(newPostData);
                await _context.SaveChangesAsync();

                return new ApiResponse(ResponseStatus.Created, new { monitor = _mapper.Map<PostFormDataResponse>(newPostData) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }
    }
}