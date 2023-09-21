using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    internal interface IServiceAccreditationService
    {
        List<ServiceAccreditationViewModel> GetServiceAccreditationViewModel();

        int CreateServiceAccreditation(ServiceViewModel serviceViewModel, List<AccreditationViewModel> accreditationViewModels);

        int UpdateServiceAccreditation(ServiceViewModel serviceViewModel, List<AccreditationViewModel> accreditationViewModels);
    }
}