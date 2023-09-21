using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IServiceCriterionSupportService
    {
        List<ServiceCriterionSupportViewModel> GetServiceCriterionSupportViewModel();

        int CreateServiceCriterionSupport(ServiceViewModel serviceViewModel, List<CriterionViewModel> criterionViewModels);

        int UpdateServiceCriterionSupport(ServiceViewModel serviceViewModel, List<CriterionViewModel> criterionViewModels);
    }
}