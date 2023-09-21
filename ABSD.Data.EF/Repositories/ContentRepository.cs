using ABSD.Data.Entities;
using ABSD.Data.IRepositories;

namespace ABSD.Data.EF.Repositories
{
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        public ContentRepository(AppDbContext context) : base(context)
        {
        }
    }
}