using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IFundingService
    {
        List<FundingViewModel> GetAll();

        FundingViewModel GetFunding(int fundingId);

        int CreateFunding(FundingViewModel fundingViewModel);

        int UpdateFunding(FundingViewModel fundingViewModel);
    }
}