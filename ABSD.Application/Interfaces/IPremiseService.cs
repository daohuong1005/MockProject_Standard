using ABSD.Application.ViewModels;
using ABSD.Common.Paging;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IPremiseService
    {
        PagedResult<PremiseViewModel> GetPremiseWithPaging(int? page, int serviceId, bool includeInActive = false);

        List<PremiseViewModel> GetByServiceId(int serviceId, bool? isActive);

        List<PremiseViewModel> GetPremiseNotLink(int serviceId);

        int AddServicePremise(List<ServicePremiseViewModel> servicePremiseViews);

        bool RemoveServicePremise(int premiseId, int serviceId);
    }
}