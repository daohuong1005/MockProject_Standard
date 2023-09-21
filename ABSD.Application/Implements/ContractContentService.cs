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
    public class ContractContentService : IContractContentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IContractContentRepository contractContentRepository;
        private readonly IContractRepository contractRepository;

        public ContractContentService(IUnitOfWork unitOfWork, IContractContentRepository contractContentRepository,
                                      IContractRepository contractRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractContentRepository = contractContentRepository;
            this.contractRepository = contractRepository;
        }

        public int CreateContractContent(List<ContentViewModel> contentViewModels, ContractViewModel contractViewModel, 
                                         ParticipationViewModel participationViewModel)
        {
            using(var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    Contract contract = new Contract()
                    {
                        ContractName = contractViewModel.ContractName
                    };
                    contractRepository.Add(contract);
                    var lastContract = contractRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault();

                    foreach (var item in contentViewModels)
                    {
                        ContractContent contractContent = new ContractContent()
                        {
                            ContractId = lastContract.Id,
                            ContentId = item.Id,
                            ParticipationId = participationViewModel.Id
                        };
                        contractContentRepository.Add(contractContent);
                    }
                    
                    unitOfWork.Commit();

                    transaction.Commit();

                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }    
        }


        public List<ContractContentViewModel> GetAllContractContent()
        {
            var query = contractContentRepository.GetAll(x => x.Participation, x => x.Content, x => x.Contract);
            var contractContentViewModelList = new List<ContractContentViewModel>();
            foreach (var item in query)
            {
                var contractContentViewModel = new ContractContentViewModel();
                contractContentViewModel.ContentId = item.ContentId;
                contractContentViewModel.ContractId = item.ContractId;
                contractContentViewModel.ParticipationId = item.ParticipationId;
                contractContentViewModel.Content = new ContentViewModel()
                {
                    Id = item.Content.Id,
                    ContentName = item.Content.ContentName,
                    Type = item.Content.Type
                };

                contractContentViewModel.Contract = new ContractViewModel()
                {
                    Id = item.Contract.Id,
                    ContractName = item.Contract.ContractName
                };

                contractContentViewModel.Participations = new ParticipationViewModel()
                {
                    Id = item.Participation.Id,
                    ParticipationName = item.Participation.ParticipationName
                };

                contractContentViewModelList.Add(contractContentViewModel);
            }

            return contractContentViewModelList;
        }

        public ContractContentViewModel GetLastestContractContentViewModel()
        {
            var query = contractContentRepository.GetAll().OrderByDescending(o => o.ContractId).FirstOrDefault();
            var contractContentViewModel = new ContractContentViewModel()
            {
                ContentId = query.ContentId,
                ContractId = query.ContractId,
                ParticipationId = query.ParticipationId
            };

            return contractContentViewModel;
        }

        public int UpdateContractContent(List<ContentViewModel> contentViewModel, ContractViewModel contractViewModel, 
                                         ParticipationViewModel participationViewModel)
        {
            //ContractContent contractContent = contractContentRepository.Single(x => x.ContractId == contractViewModel.Id);
            //contractContent.ContractId = contractViewModel.Id;
            //contractContent.ContentId = contentViewModel.Id;
            //contractContent.ParticipationId = participationViewModel.Id;
            //contractContentRepository.Update(contractContent);

            return unitOfWork.Commit();
        }
    }
}