using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UpMo.Common.DTO.Request.Organization;
using UpMo.Common.DTO.Response.Organization;
using UpMo.Common.Response;
using UpMo.Data;
using UpMo.Data.Extensions;
using UpMo.Entities;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class ManagerService : BaseService, IManagerService
    {
        public ManagerService(IMapper mapper,
                                          UpMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(ManagerCreateRequest request)
        {
            var organization = await _context.Organizations.SingleOrDefaultAsync(x => x.ID == request.OrganizationID);

            if (organization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            if (organization.CheckFounder(request.AuthenticatedUserID))
            {
                var willBeManagerUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Identifier
                                                                                    || x.Email == request.Identifier);
                if (willBeManagerUser is null)
                {
                    return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundUser);
                }

                bool userAlreadyManagerAtGivenOrganization = await _context.Managers
                     .AnyAsync(x => x.UserID == willBeManagerUser.Id && x.OrganizationID == organization.ID);

                if (userAlreadyManagerAtGivenOrganization) // todo: take precautions too on database side
                {
                    return new ApiResponse(ResponseStatus.BadRequest, ResponseMessage.AlreadyManager);
                }

                var newManager = _mapper.Map<Manager>(request);
                newManager.UserID = willBeManagerUser.Id;
                await _context.AddAsync(newManager);
                await _context.SaveChangesAsync();

                return new ApiResponse(ResponseStatus.Created, new { Manager = _mapper.Map<ManagerResponse>(newManager) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> UpdateByRequestAsync(ManagerUpdateRequest request)
        {
            var toBeUpdatedManager = await _context.Managers.Include(x => x.Organization)
                                                                        .SingleOrDefaultAsync(x =>
                                                                            x.ID == request.ID
                                                                            && x.OrganizationID == request.OrganizationID);

            if (toBeUpdatedManager is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundManager);
            }

            if (toBeUpdatedManager.Organization.CheckFounder(request.AuthenticatedUserID))
            {
                toBeUpdatedManager = _mapper.Map(request, toBeUpdatedManager);
                await _context.SaveChangesAsync();
                return new ApiResponse(ResponseStatus.OK, new { Manager = _mapper.Map<ManagerResponse>(toBeUpdatedManager) });
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
        }

        public async Task<ApiResponse> SoftDeleteByIDsAsync(Guid organizationID, Guid managerID, int authenticatedUserID)
        {
            var toBeSoftDeletedManager = await _context.Managers.Include(x => x.Organization)
                                                                            .SingleOrDefaultAsync(x =>
                                                                                x.ID == managerID
                                                                                && x.OrganizationID == organizationID);
            if (toBeSoftDeletedManager is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundManager);
            }

            if (toBeSoftDeletedManager.Organization.CheckFounder(authenticatedUserID))
            {
                toBeSoftDeletedManager.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return new ApiResponse(ResponseStatus.NoContent);
            }

            return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
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