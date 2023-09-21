using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ContractContentRepository : Repository<ContractContent>, IContractContentRepository
    {
        public ContractContentRepository(AppDbContext context) : base(context)
        {
        }
    }
}