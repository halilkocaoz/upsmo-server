using Microsoft.EntityFrameworkCore;

namespace UpsMo.Organization.Data.Repositories
{
    // todo base repository with common methods and unit of work
    public interface IOrganizationRepository
    {
        Task AddAsync(Data.Models.Organization organization);
        Task<Data.Models.Organization> GetByIDAsync(Guid organizationID);
        Task<List<Data.Models.Organization>> GetAllWithManagersByUserIDAsync(int userID);
    }

    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DataContext _context;
        public OrganizationRepository(DataContext context)
            => _context = context;

        public async Task AddAsync(Models.Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
        }

        public Task<List<Models.Organization>> GetAllWithManagersByUserIDAsync(int userID)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Organization> GetByIDAsync(Guid organizationID)
        {
            return await _context.Organizations.SingleAsync(o => o.ID == organizationID);
        }
    }
}