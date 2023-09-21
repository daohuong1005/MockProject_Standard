using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABSD.Application.Implements
{
    public class ServiceCriterionSupportService : IServiceCriterionSupportService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceCriterionSupportRepository serviceCriterionSupportRepository;
        private readonly IServiceRepository serviceRepository;

        public ServiceCriterionSupportService(IUnitOfWork unitOfWork, IServiceCriterionSupportRepository serviceCriterionSupportRepository,
                                              IServiceRepository serviceRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceCriterionSupportRepository = serviceCriterionSupportRepository;
            this.serviceRepository = serviceRepository;
        }

        public int CreateServiceCriterionSupport(ServiceViewModel serviceViewModel, List<CriterionViewModel> criterionViewModels)
        {
            var addedService = serviceRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault();

            var serviceCriterionSupportList = new List<ServiceCriterionSupport>();
            foreach (var item in criterionViewModels)
            {
                serviceCriterionSupportList.Add(new ServiceCriterionSupport()
                {
                    CriterionId = item.Id,
                    ServiceId = addedService.Id
                });
            }
            serviceCriterionSupportRepository.AddRange(serviceCriterionSupportList);
            return unitOfWork.Commit();
        }

        public List<ServiceCriterionSupportViewModel> GetServiceCriterionSupportViewModel()
        {
            var query = serviceCriterionSupportRepository.GetAll(x => x.Criterion, x => x.Service);
            var serviceCriterionViewModelsList = new List<ServiceCriterionSupportViewModel>();
            foreach (var item in query)
            {
                var serviceCriterionSupportViewModel = new ServiceCriterionSupportViewModel();
                serviceCriterionSupportViewModel.CriterionId = item.CriterionId;
                serviceCriterionSupportViewModel.ServiceId = item.ServiceId;
                serviceCriterionSupportViewModel.Criterion = new CriterionViewModel()
                {
                    Id = item.Criterion.Id,
                    Name = item.Criterion.Name
                };

                serviceCriterionSupportViewModel.Service = new ServiceViewModel()
                {
                    Id = item.Service.Id,
                    ServiceName = item.Service.ServiceName
                };

                serviceCriterionViewModelsList.Add(serviceCriterionSupportViewModel);
            }

            return serviceCriterionViewModelsList;
        }
        
        public int UpdateServiceCriterionSupport(ServiceViewModel serviceViewModel, List<CriterionViewModel> criterionViewModels)
        {
            using(var transaction = unitOfWork.Context.Database.BeginTransaction())
            {
                try
                {
                    var query = serviceCriterionSupportRepository.GetMany(x => x.ServiceId == serviceViewModel.Id).ToList();
                    serviceCriterionSupportRepository.RemoveRange(query);
                    var serviceCriterionSupportList = new List<ServiceCriterionSupport>();
                    foreach (var item in criterionViewModels)
                    {
                        serviceCriterionSupportList.Add(new ServiceCriterionSupport()
                        {
                            CriterionId = item.Id,
                            ServiceId = serviceViewModel.Id
                        });
                    }
                    serviceCriterionSupportRepository.AddRange(serviceCriterionSupportList);
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
