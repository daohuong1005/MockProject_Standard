using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IContractService
    {
        List<ContractViewModel> GetContractViewModel();

        int CreateContract(ContractViewModel contractViewModel);

        int UpdateContract(ContractViewModel contractViewModel);

        ContractViewModel GetLastestContractViewModel();
    }
}