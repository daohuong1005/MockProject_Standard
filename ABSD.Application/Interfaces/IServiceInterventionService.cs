using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    internal interface IServiceInterventionService
    {
        List<ServiceInterventionViewModel> GetServiceInterventionViewModel();

        int CreateServiceIntervention(ServiceViewModel serviceViewModel, List<InterventionViewModel> interventionViewModel);

        int UpdateServiceIntervention(ServiceViewModel serviceViewModel, List<InterventionViewModel> interventionViewModel);
    }
}