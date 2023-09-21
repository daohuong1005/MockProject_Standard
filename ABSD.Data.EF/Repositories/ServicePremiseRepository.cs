using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ServicePremiseRepository : Repository<ServicePremises>, IServicePremiseRepository
    {
        public ServicePremiseRepository(AppDbContext context) : base(context)
        {
        }
    }
}