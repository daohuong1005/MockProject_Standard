using ABSD.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABSD.Application.Interfaces
{
    public interface ICriterionService
    {
        List<CriterionViewModel> GetCriterionViewModel();

        int CreateCriterion(CriterionViewModel criterionViewModel);

        int UpdateCriterion(CriterionViewModel criterionViewModel);
    }
}
