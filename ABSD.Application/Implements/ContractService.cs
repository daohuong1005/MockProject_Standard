using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository contractRepository;
        private readonly IUnitOfWork unitOfWork;

        public ContractService(IUnitOfWork unitOfWork, IContractRepository contractRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractRepository = contractRepository;
        }

        public int CreateContract(ContractViewModel contractViewModel)
        {
            Contract contract = new Contract()
            {
                Id = contractViewModel.Id,
                ContractName = contractViewModel.ContractName,
                Service = new Service(),
            };

            contractRepository.Add(contract);

            return unitOfWork.Commit();
        }

        public List<ContractViewModel> GetContractViewModel()
        {
            var query = contractRepository.GetAll(x => x.ContractContents);

            var contractViewModelsList = new List<ContractViewModel>();
            foreach (var item in query)
            {
                var contractViewModel = new ContractViewModel();
                contractViewModel.Id = item.Id;
                contractViewModel.ContractName = item.ContractName;
                contractViewModel.Service = new ServiceViewModel()
                {
                    Id = item.Service.Id,
                    ServiceName = item.Service.ServiceName
                };

                foreach (var contractContent in item.ContractContents)
                {
                    contractViewModel.ContractContents.Add(new ContractContentViewModel()
                    {
                        ContentId = contractContent.ContentId,
                        ParticipationId = contractContent.ParticipationId,
                        ContractId = contractContent.ContractId
                    });
                };

                contractViewModelsList.Add(contractViewModel);
            }

            return contractViewModelsList;
        }

        public ContractViewModel GetLastestContractViewModel()
        {
            var contract = contractRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault();
            var contractViewModel = new ContractViewModel()
            {
                Id = contract.Id,
            };

            return contractViewModel;
        }

        public int UpdateContract(ContractViewModel contractViewModel)
        {
            throw new NotImplementedException();
        }
    }
}