using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UpMo.Common.DTO.Request;
using UpMo.Common.Response;
using UpMo.Data;
using UpMo.Data.Extensions;
using UpMo.Entities;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class OrganizationManagerService : BaseService, IOrganizationManagerService
    {
        public OrganizationManagerService(IMapper mapper,
                                          UpMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(OrganizationManagerCreateRequest request)
        {
            var toBeCreatedAManagerOrganization = await _context.Organizations.SingleOrDefaultAsync(x => x.ID == request.OrganizationID);

            if (toBeCreatedAManagerOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            if (toBeCreatedAManagerOrganization.CheckCreator(request.AuthenticatedUserID))
            {
                var willBeManagerUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Identifier
                                                                                    || x.Email == request.Identifier);
                if (willBeManagerUser is null)
                {
                    return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundUser);
                }

                bool userAlreadyManagerAtGivenOrganization = await _context.OrganizationManagers
                     .AnyAsync(x => x.UserID == willBeManagerUser.Id && x.OrganizationID == toBeCreatedAManagerOrganization.ID);

                if (userAlreadyManagerAtGivenOrganization) // todo: take precautions too on database side
                {
                    return new ApiResponse(ResponseStatus.BadRequest, ResponseMessage.AlreadyManager);
                }

                var newManager = _mapper.Map<OrganizationManager>(request);
                newManager.UserID = willBeManagerUser.Id;
                await _context.AddAsync(newManager);
                await _context.SaveChangesAsync();

                return new ApiResponse(ResponseStatus.Created);
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> UpdateByRequestAsync(OrganizationManagerUpdateRequest request)
        {
            var toBeUpdatedManager = await _context.OrganizationManagers.Include(x => x.Organization)
                                                                                    .SingleOrDefaultAsync(x => x.ID == request.OrganizationManagerID);

            if (toBeUpdatedManager is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            if (toBeUpdatedManager.Organization.CheckCreator(request.AuthenticatedUserID))
            {
                toBeUpdatedManager.Admin = request.Admin;
                toBeUpdatedManager.Viewer = request.Viewer;
                await _context.SaveChangesAsync();
                return new ApiResponse(ResponseStatus.OK);
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);;
        }
    }
}