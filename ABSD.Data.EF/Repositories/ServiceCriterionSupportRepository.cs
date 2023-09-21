using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ServiceCriterionSupportRepository : Repository<ServiceCriterionSupport>, IServiceCriterionSupportRepository
    {
        public ServiceCriterionSupportRepository(AppDbContext context) : base(context)
        {
        }
    }
}