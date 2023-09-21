using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class RoleOrganizationRepository : Repository<RoleOrganization>, IRoleOrganizationRepository
    {
        public RoleOrganizationRepository(AppDbContext context) : base(context)
        {
        }
    }
}