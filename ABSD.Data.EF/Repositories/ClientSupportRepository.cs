using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ClientSupportRepository : Repository<ClientSupport>, IClientSupportRepository
    {
        public ClientSupportRepository(AppDbContext context) : base(context)
        {
        }
    }
}