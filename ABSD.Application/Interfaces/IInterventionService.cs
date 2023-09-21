using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IInterventionService
    {
        List<InterventionViewModel> GetInterventionViewModel();

        int Createintervention(InterventionViewModel interventionViewModel);

        int Updateintervention(InterventionViewModel interventionViewModel);
    }
}