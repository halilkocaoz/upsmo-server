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
    public class OrganizationService : BaseService, IOrganizationService
    {
        public OrganizationService(IMapper mapper,
                                   UpsMoContext context) : base(mapper, context)
        {
        }

        public async Task<ApiResponse> CreateByRequestAsync(OrganizationRequest request)
        {
            var organization = _mapper.Map<Organization>(request);

            var manager = new Manager
            {
                UserID = organization.FounderUserID,
                Admin = true,
                Viewer = true
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                await _context.AddAsync(organization);
                manager.OrganizationID = organization.ID;
                await _context.AddAsync(manager);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return new ApiResponse(ResponseStatus.Created, new { organization = _mapper.Map<OrganizationResponse>(organization) });
        }

        public async Task<ApiResponse> UpdateByRequestAsync(OrganizationRequest request)
        {
            var toBeUpdatedOrganization = await _context.Organizations.Include(x => x.Managers)
                                                                      .SingleOrDefaultAsync(x => x.ID == request.ID);
            if (toBeUpdatedOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            bool userHasPermissionToUpdate = toBeUpdatedOrganization.CheckFounderOrAdmin(request.AuthenticatedUserID);
            if (userHasPermissionToUpdate is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            toBeUpdatedOrganization.Name = request.Name;
            await _context.SaveChangesAsync();
            return new ApiResponse(ResponseStatus.OK, new { organization = _mapper.Map<OrganizationResponse>(toBeUpdatedOrganization) });
        }

        public async Task<ApiResponse> SoftDeleteByIDAsync(Guid organizationID, int authenticatedUserID)
        {
            var toBeSofDeletedOrganization = await _context.Organizations.Include(x=> x.Monitors).SingleOrDefaultAsync(x => x.ID == organizationID);
            if (toBeSofDeletedOrganization is null)
            {
                return new ApiResponse(ResponseStatus.NotFound, ResponseMessage.NotFoundOrganization);
            }

            bool userHasPermissionToSoftDelete = toBeSofDeletedOrganization.CheckFounder(authenticatedUserID);
            if (userHasPermissionToSoftDelete is false)
            {
                return new ApiResponse(ResponseStatus.Forbid, ResponseMessage.Forbid);
            }

            var dateTimeNowUtc = DateTime.UtcNow;
            toBeSofDeletedOrganization.DeletedAt = dateTimeNowUtc;
            // mark as deleted the related monitors with deleted organization too
            foreach (var monitor in toBeSofDeletedOrganization.Monitors)
            {
                monitor.DeletedAt = dateTimeNowUtc;
            }
            await _context.SaveChangesAsync();
            return new ApiResponse(ResponseStatus.NoContent);
        }

        public async Task<ApiResponse> GetOrganizationsByAuthenticatedUserIDAsync(int authenticatedUserID)
        {
            var organizationsForAuthenticatedUser = await _context.Organizations
                                                    .Include(x => x.Managers)
                                                    .Include(x => x.Monitors).ThenInclude(x => x.PostForms)
                                                    .Include(x => x.Monitors).ThenInclude(x => x.Headers)
                                                    .AsSplitQuery().Where(x =>
                                                                   x.FounderUserID == authenticatedUserID
                                                                || x.Managers.Any(x => x.Viewer
                                                                && x.UserID == authenticatedUserID))
                                                    .ToListAsync();

            object returnObject = new { organizations = _mapper.Map<List<OrganizationResponse>>(organizationsForAuthenticatedUser) };
            return new ApiResponse(ResponseStatus.OK, returnObject);
        }
    }
}