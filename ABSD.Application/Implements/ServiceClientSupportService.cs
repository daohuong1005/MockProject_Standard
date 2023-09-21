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
    public class ServiceClientSupportService : IServiceClientSupportService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceClientSupportRepository serviceClientSupportRepository;
        private readonly IServiceRepository serviceRepository;

        public ServiceClientSupportService(IUnitOfWork unitOfWork, IServiceClientSupportRepository serviceClientSupportRepository,
                                           IServiceRepository serviceRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceClientSupportRepository = serviceClientSupportRepository;
            this.serviceRepository = serviceRepository;
        }

        public int CreateServiceClientSupport(ServiceViewModel serviceViewModel, List<ClientSupportViewModel> clientSupportViewModels)
        {
            var addedService = serviceRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault();
            var serviceClientSupportList = new List<ServiceClientSupport>();
            foreach (var item in clientSupportViewModels)
            {
                serviceClientSupportList.Add(new ServiceClientSupport()
                {
                    ClientSupportId = item.Id,
                    ServiceId = addedService.Id
                });
            }
            serviceClientSupportRepository.AddRange(serviceClientSupportList);

            return unitOfWork.Commit();
        }

        public List<ServiceClientSupportViewModel> GetServiceClientSupportViewModel()
        {
            var query = serviceClientSupportRepository.GetAll(x=>x.ClientSupport, x=>x.Service);
            var serviceClientSupportViewModelsList = new List<ServiceClientSupportViewModel>();
            foreach (var item in query)
            {
                var serviceClientSupportViewModel = new ServiceClientSupportViewModel();
                serviceClientSupportViewModel.ClientSupportId = item.ClientSupportId;
                serviceClientSupportViewModel.ServiceId = item.ServiceId;
                serviceClientSupportViewModel.ClientSupport = new ClientSupportViewModel()
                {
                    Id = item.ClientSupport.Id,
                    Name = item.ClientSupport.Name
                };
                serviceClientSupportViewModel.Service = new ServiceViewModel()
                {
                    Id = item.ServiceId,
                    ServiceName = item.Service.ServiceName
                };

                serviceClientSupportViewModelsList.Add(serviceClientSupportViewModel);
            }

            return serviceClientSupportViewModelsList;
        }

        public int UpdateServiceClientSupport(ServiceViewModel serviceViewModel, List<ClientSupportViewModel> serviceClientSupportViewModels)
        {
            using(var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var query = serviceClientSupportRepository.GetMany(x => x.ServiceId == serviceViewModel.Id).ToList();
                    serviceClientSupportRepository.RemoveRange(query);
                    var serviceClientSupportList = new List<ServiceClientSupport>();
                    foreach (var item in serviceClientSupportViewModels)
                    {
                        var serviceClientSupport = new ServiceClientSupport()
                        {
                            ClientSupportId = item.Id,
                            ServiceId = serviceViewModel.Id
                        };
                        serviceClientSupportList.Add(serviceClientSupport);
                    }
                    serviceClientSupportRepository.AddRange(serviceClientSupportList);
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