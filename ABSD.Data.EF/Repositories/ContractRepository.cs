using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        public ContractRepository(AppDbContext context) : base(context)
        {
        }
    }
}