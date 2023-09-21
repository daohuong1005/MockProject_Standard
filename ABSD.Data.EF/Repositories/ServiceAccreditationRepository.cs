using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ServiceAccreditationRepository : Repository<ServiceAccreditation>, IServiceAccreditationRepository
    {
        public ServiceAccreditationRepository(AppDbContext context) : base(context)
        {
        }
    }
}