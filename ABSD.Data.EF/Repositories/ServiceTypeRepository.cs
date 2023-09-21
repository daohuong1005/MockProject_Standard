using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ServiceTypeRepository : Repository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}