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
    public class ServiceAccreditationService : IServiceAccreditationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceAccreditationRepository serviceAccreditationRepository;
        private readonly IServiceRepository serviceRepository;

        public ServiceAccreditationService(IUnitOfWork unitOfWork, IServiceAccreditationRepository serviceAccreditationRepository,
                                           IServiceRepository serviceRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceAccreditationRepository = serviceAccreditationRepository;
            this.serviceRepository = serviceRepository;
        }

        public int CreateServiceAccreditation(ServiceViewModel serviceViewModel, List<AccreditationViewModel> accreditationViewModels)
        {
            using (var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    Service service1 = new Service()
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
                    serviceRepository.Add(service1);
                    var addedService = serviceRepository.GetAll().OrderByDescending(o => o.Id).Take(1).ToList();

                    var serviceAccreditationList = new List<ServiceAccreditation>();
                    foreach (var item in accreditationViewModels)
                    {
                        serviceAccreditationList.Add(new ServiceAccreditation()
                        {
                            AccreditationId = item.Id,
                            ServiceId = addedService[0].Id
                        });
                    }
                    serviceAccreditationRepository.AddRange(serviceAccreditationList);
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

        public List<ServiceAccreditationViewModel> GetServiceAccreditationViewModel()
        {
            var query = serviceAccreditationRepository.GetAll(x => x.Accreditation, x => x.Service);
            var serviceAccreditationViewModelsList = new List<ServiceAccreditationViewModel>();
            foreach (var item in query)
            {
                var serviceAccreditationViewModel = new ServiceAccreditationViewModel();
                serviceAccreditationViewModel.AccreditationId = item.AccreditationId;
                serviceAccreditationViewModel.ServiceId = item.ServiceId;
                serviceAccreditationViewModel.Accreditation = new AccreditationViewModel()
                {
                    Id = item.Accreditation.Id,
                    Name = item.Accreditation.Name
                };
                serviceAccreditationViewModel.Service = new ServiceViewModel()
                {
                    Id = item.Service.Id,
                    ServiceName = item.Service.ServiceName
                };
                serviceAccreditationViewModelsList.Add(serviceAccreditationViewModel);
            }

            return serviceAccreditationViewModelsList;
        }

        public int UpdateServiceAccreditation(ServiceViewModel serviceViewModel, List<AccreditationViewModel> accreditationViewModels)
        {
            using(var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var query = serviceAccreditationRepository.GetMany(x => x.ServiceId == serviceViewModel.Id).ToList();
                    serviceAccreditationRepository.RemoveRange(query);
                    
                    var serviceAccreditationList = new List<ServiceAccreditation>();
                    foreach (var item in accreditationViewModels)
                    {
                        serviceAccreditationList.Add(new ServiceAccreditation()
                        {
                            AccreditationId = item.Id,
                            ServiceId = serviceViewModel.Id
                        });
                    }
                    serviceAccreditationRepository.AddRange(serviceAccreditationList);
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