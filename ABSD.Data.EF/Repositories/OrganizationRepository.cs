using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ABSD.Data.EF.Repositories
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Organization> GetByServiceId(int serviceId)
        {
            var organiztion = context.ServiceOrganizations.
               Where(x => x.ServiceId == serviceId).Include(x => x.Organization).Select(x => x.Organization);

            return organiztion;
        }

        public IQueryable<RoleOrganization> GetRoleByOrganizationId(int organizationId)
        {
            return context.RoleOrganizations.Where(x => x.OrganizationId == organizationId).Include(x => x.AppRole);
        }
    }
}