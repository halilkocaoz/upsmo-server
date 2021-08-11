using System.Threading.Tasks;
using AutoMapper;
using UpMo.Common.DTO.Request;
using UpMo.Common.DTO.Response;
using UpMo.Common.Response;
using UpMo.Data;
using UpMo.Entities;
using UpMo.Services.Abstract;

namespace UpMo.Services.Concrete
{
    public class OrganizationService : IOrganizationService
    {
        private readonly UpMoContext _context;
        private readonly IMapper _mapper;

        public OrganizationService(UpMoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(OrganizationCreateRequest request)
        {
            var organization = _mapper.Map<Organization>(request);

            var organizationManager = new OrganizationManager // todo mapper
            {
                ID = System.Guid.NewGuid(),
                OrganizationID = organization.ID,
                UserID = organization.CreatorUserID
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                await _context.AddAsync(organization);
                await _context.AddAsync(organizationManager);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return new ApiResponse(ResponseStatus.Created, _mapper.Map<OrganizationResponse>(organization));
        }
    }
}