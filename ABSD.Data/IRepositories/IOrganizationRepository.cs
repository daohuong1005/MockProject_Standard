using ABSD.Data.Entities;
using System.Linq;

namespace ABSD.Data.IRepositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        IQueryable<Organization> GetByServiceId(int serviceId);

        IQueryable<RoleOrganization> GetRoleByOrganizationId(int organizationId);
    }
}