using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IContractContentService
    {
        List<ContractContentViewModel> GetAllContractContent();

        int CreateContractContent(List<ContentViewModel> contentViewModel, ContractViewModel contractViewModel, 
                                  ParticipationViewModel participationViewModel);

        int UpdateContractContent(List<ContentViewModel> contentViewModel, ContractViewModel contractViewModel, 
                                  ParticipationViewModel participationViewModel);
        ContractContentViewModel GetLastestContractContentViewModel();
    }
}