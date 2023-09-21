using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IAccreditationService
    {
        List<AccreditationViewModel> GetAccreditationViewModel();

        int CreateAccreditation(AccreditationViewModel accreditationViewModel);

        int UpdateAccreditationViewModel(AccreditationViewModel accreditationViewModel);
    }
}