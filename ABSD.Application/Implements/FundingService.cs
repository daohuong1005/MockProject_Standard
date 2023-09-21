using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data.Entities;
using System;
using System.Collections.Generic;

namespace ABSD.Application.Implements
{
    public class FundingService : IFundingService
    {
        public int CreateFunding(FundingViewModel fundingViewModel)
        {
            Funding funding = new Funding();
            funding.FundingSource = fundingViewModel.FundingSource;
            return 1;
        }

        public List<FundingViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public FundingViewModel GetFunding(int fundingId)
        {
            throw new NotImplementedException();
        }

        public int UpdateFunding(FundingViewModel fundingViewModel)
        {
            throw new NotImplementedException();
        }
    }
}