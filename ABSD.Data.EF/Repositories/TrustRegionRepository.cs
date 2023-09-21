using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class TrustRegionRepository : Repository<TrustRegion>, ITrustRegionRepository
    {
        public TrustRegionRepository(AppDbContext context) : base(context)
        {
        }
    }
}