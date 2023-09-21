using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ServiceClientSupportRepository : Repository<ServiceClientSupport>, IServiceClientSupportRepository
    {
        public ServiceClientSupportRepository(AppDbContext context) : base(context)
        {
        }
    }
}