using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UpsMo.Common.DTO.Request.Organization;
using UpsMo.Common.DTO.Response.Organization;
using UpsMo.Common.Response;
using UpsMo.Data;
using UpsMo.Data.Extensions;
using UpsMo.Entities;
using UpsMo.Services.Abstract;

namespace UpsMo.Services.Concrete
{
    public class ManagerService : BaseService, IManagerService
    {
        public ManagerService(IMapper mapper,
                                          UpsMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(ManagerCreateRequest request)
        {
            var toBeRelatedOrganization = await _context.Organizations.SingleOrDefaultAsync(x => x.ID == request.OrganizationID);
            if (toBeRelatedOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            bool hasUserPermissionToCreateManagerForOrganization = toBeRelatedOrganization.CheckFounder(request.AuthenticatedUserID);
            if (hasUserPermissionToCreateManagerForOrganization is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            var willBeManagerUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Identifier
                                                                                  || x.Email == request.Identifier);
            if (willBeManagerUser is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundUser);
            }

            bool userAlreadyManagerAtGivenOrganization = await _context.Managers
                 .AnyAsync(x => x.UserID == willBeManagerUser.Id && x.OrganizationID == toBeRelatedOrganization.ID);

            if (userAlreadyManagerAtGivenOrganization is true) // todo: take precautions too on database side
            {
                return new ApiResponse(ResponseStatus.BadRequest, ResponseMessage.AlreadyManager);
            }

            var newManager = _mapper.Map<Manager>(request);
            newManager.UserID = willBeManagerUser.Id;
            await _context.AddAsync(newManager);
            await _context.SaveChangesAsync();

            return new ApiResponse(ResponseStatus.Created, new { Manager = _mapper.Map<ManagerResponse>(newManager) });
        }

        private async Task<Manager> getManagerByIDsAsync(Guid managerID, Guid organizationID) =>
        await _context.Managers.Include(x => x.Organization)
                               .SingleOrDefaultAsync(x => x.ID == managerID
                                                          && x.OrganizationID == organizationID);

        public async Task<ApiResponse> UpdateByRequestAsync(ManagerUpdateRequest request)
        {
            var toBeUpdatedManager = await getManagerByIDsAsync(request.ID, request.OrganizationID);
            if (toBeUpdatedManager is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundManager);
            }

            bool hasUserPermissionToUpdate = toBeUpdatedManager.Organization.CheckFounder(request.AuthenticatedUserID);
            if (hasUserPermissionToUpdate is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            toBeUpdatedManager = _mapper.Map(request, toBeUpdatedManager);
            await _context.SaveChangesAsync();
            return new ApiResponse(ResponseStatus.OK, new { Manager = _mapper.Map<ManagerResponse>(toBeUpdatedManager) });
        }

        public async Task<ApiResponse> SoftDeleteByIDsAsync(Guid organizationID, Guid managerID, int authenticatedUserID)
        {
            var toBeSoftDeletedManager = await getManagerByIDsAsync(managerID, organizationID);
            if (toBeSoftDeletedManager is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundManager);
            }

            bool hasUserPermissionToSoftDelete = toBeSoftDeletedManager.Organization.CheckFounder(authenticatedUserID);
            if (hasUserPermissionToSoftDelete is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            toBeSoftDeletedManager.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return new ApiResponse(ResponseStatus.NoContent);
        }

        public async Task<ApiResponse> GetManagersByOrganizationIDAndAuthenticatedUserID(Guid organizationID, int authenticatedUserID)
        {
            var managersForAuthenticatedUser = await _context.Managers
                                                    .Include(x => x.Organization)
                                                    .Include(x => x.User)
                                                    .Where(x => x.OrganizationID == organizationID
                                                                && x.Organization.FounderUserID == authenticatedUserID)
                                                    .ToListAsync();

            object returnObject = new { Managers = _mapper.Map<List<ManagerResponse>>(managersForAuthenticatedUser) };
            return new ApiResponse(ResponseStatus.OK, returnObject);
        }


    }
}