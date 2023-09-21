using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class CriterionRepository : Repository<Criterion>, ICriterionRepository
    {
        public CriterionRepository(AppDbContext context) : base(context)
        {
        }
    }
}