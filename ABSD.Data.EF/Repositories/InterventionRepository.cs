using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class InterventionRepository : Repository<Intervention>, IInterventionRepository
    {
        public InterventionRepository(AppDbContext context) : base(context)
        {
        }
    }
}