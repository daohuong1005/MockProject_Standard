using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ABSD.Data.EF.Repositories
{
    public class PremiseRepository : Repository<Premise>, IPremiseRepository
    {
        public PremiseRepository(AppDbContext context) : base(context)
        {
        }

        public void AddServicePremise(ServicePremises servicePremises)
        {
            context.ServicePremises.Add(servicePremises);
        }

        public IQueryable<Premise> GetByServiceId(int serviceId)
        {
            return context.ServicePremises.Where(x => x.ServiceId == serviceId).Include(x => x.Premise).Select(x => x.Premise);
        }
    }
}