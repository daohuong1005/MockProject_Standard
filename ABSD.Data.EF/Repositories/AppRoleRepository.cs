using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class AppRoleRepository : Repository<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}