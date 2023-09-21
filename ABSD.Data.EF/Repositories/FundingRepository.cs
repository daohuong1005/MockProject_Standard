using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class FundingRepository : Repository<Funding>, IFundingRepository
    {
        public FundingRepository(AppDbContext context) : base(context)
        {
        }
    }
}