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
    public class ServiceInterventionService : IServiceInterventionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceInterventionRepository serviceInterventionRepository;
        private readonly IServiceRepository serviceRepository;

        public ServiceInterventionService(IUnitOfWork unitOfWork, IServiceInterventionRepository serviceInterventionRepository,
                                          IServiceRepository serviceRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceInterventionRepository = serviceInterventionRepository;
            this.serviceRepository = serviceRepository;
        }

        public int CreateServiceIntervention(ServiceViewModel serviceViewModel, List<InterventionViewModel> interventionViewModel)
        {
            using(var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    Service service = new Service()
                    {
                        ServiceName = serviceViewModel.ServiceName,
                        ServiceShortDescription = serviceViewModel.ServiceShortDescription,
                        FundingId = serviceViewModel.FundingId,
                        ContactId = serviceViewModel.ContactId,
                        ClientDescription = serviceViewModel.ClientDescription,
                        ParticipationId = serviceViewModel.ParticipationViewModel.Id,
                        ServiceStartExpected = serviceViewModel.ServiceStartExpected,
                        ServiceStartDate = serviceViewModel.ServiceStartDate,
                        ServiceEndDate = serviceViewModel.ServiceEndDate,
                        ServiceExtendable = serviceViewModel.ServiceExtendable,
                        ServiceActive = true,
                        ServiceFullDescription = serviceViewModel.ServiceFullDescription,
                        DeptCode = serviceViewModel.DeptCode,
                        ServiceTypeId = serviceViewModel.ServiceTypeId,
                        ServiceDesscriptionDelivery = serviceViewModel.ServiceDesscriptionDelivery,
                        ServiceContactCode = serviceViewModel.ServiceContactCode,
                        ServiceContractValue = serviceViewModel.ServiceContractValue,
                        ContractStagedPayment = serviceViewModel.ContractStagedPayment,
                        ReferralProcess = serviceViewModel.ReferralProcess,
                        ServiceTimeLimited = serviceViewModel.ServiceTimeLimited
                    };
                    serviceRepository.Add(service);
                    var addedService = serviceRepository.GetAll().OrderByDescending(o => o.Id).Take(1).ToList();
                   
                    var serviceInterventionList = new List<ServiceIntervention>();
                    foreach (var item in interventionViewModel)
                    {
                        serviceInterventionList.Add(new ServiceIntervention()
                        {
                            InterventionId = item.Id,
                            ServiceId = addedService[0].Id
                        });
                    }
                    serviceInterventionRepository.AddRange(serviceInterventionList);
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

        public List<ServiceInterventionViewModel> GetServiceInterventionViewModel()
        {
            var query = serviceInterventionRepository.GetAll(x => x.Intervention, x => x.Service);
            var serviceInterventionViewModelsList = new List<ServiceInterventionViewModel>();
            foreach (var item in query)
            {
                var serviceInterventionViewModel = new ServiceInterventionViewModel();
                serviceInterventionViewModel.InterventionId = item.InterventionId;
                serviceInterventionViewModel.ServiceId = item.ServiceId;
                serviceInterventionViewModel.Service = new ServiceViewModel()
                {
                    Id = item.Service.Id,
                    ServiceName = item.Service.ServiceName
                };
                serviceInterventionViewModel.Intervention = new InterventionViewModel()
                {
                    Id = item.Intervention.Id,
                    InterventionName = item.Intervention.InterventionName
                };
                serviceInterventionViewModelsList.Add(serviceInterventionViewModel);
            }

            return serviceInterventionViewModelsList;
        }

        public int UpdateServiceIntervention(ServiceViewModel serviceViewModel, List<InterventionViewModel> interventionViewModel)
        {
            using(var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var query = serviceInterventionRepository.GetMany(x => x.ServiceId == serviceViewModel.Id).ToList();
                    serviceInterventionRepository.RemoveRange(query);
                    var serviceInterventionList = new List<ServiceIntervention>();
                    foreach (var item in interventionViewModel)
                    {
                        serviceInterventionList.Add(new ServiceIntervention()
                        {
                            InterventionId = item.Id,
                            ServiceId = serviceViewModel.Id
                        });
                    }
                    serviceInterventionRepository.AddRange(serviceInterventionList);
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
    }
}