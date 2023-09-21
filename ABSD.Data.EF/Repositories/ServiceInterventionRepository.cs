using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ServiceInterventionRepository : Repository<ServiceIntervention>, IServiceInterventionRepository
    {
        public ServiceInterventionRepository(AppDbContext context) : base(context)
        {
        }
    }
}