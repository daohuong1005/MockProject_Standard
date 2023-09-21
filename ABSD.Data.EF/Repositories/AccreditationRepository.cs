using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class AccreditationRepository : Repository<Accreditation>, IAccreditationRepository
    {
        public AccreditationRepository(AppDbContext context) : base(context)
        {
        }
    }
}