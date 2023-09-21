using ABSD.Data.Entities;
using System.Linq;

namespace ABSD.Data.IRepositories
{
    public interface IPremiseRepository : IRepository<Premise>
    {
        IQueryable<Premise> GetByServiceId(int serviceId);

        void AddServicePremise(ServicePremises servicePremises);
    }
}