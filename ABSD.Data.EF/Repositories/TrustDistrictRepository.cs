using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class TrustDistrictRepository : Repository<TrustDistrict>, ITrustDistrictRepository
    {
        public TrustDistrictRepository(AppDbContext context) : base(context)
        {
        }
    }
}