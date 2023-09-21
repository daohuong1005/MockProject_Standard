using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Constants;
using ABSD.Common.Paging;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class PremiseService : IPremiseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPremiseRepository premiseRepository;
        private readonly IServicePremiseRepository servicePremiseRepository;
        private readonly IMapper _mapper;

        public PremiseService(IUnitOfWork unitOfWork, IPremiseRepository premiseRepository,
            IMapper mapper, IServicePremiseRepository servicePremiseRepository)
        {
            this.unitOfWork = unitOfWork;
            this.premiseRepository = premiseRepository;
            this.servicePremiseRepository = servicePremiseRepository;
            _mapper = mapper;
        }

        public List<PremiseViewModel> GetByServiceId(int serviceId, bool? isActive)
        {
            if (serviceId <= 0)
                return null;
            var servicePremises = servicePremiseRepository.GetMany(x => x.ServiceId == serviceId, x => x.Premise);

            if (servicePremises.Count() <= 0)
                return null;
            var premisesViewModels = new List<PremiseViewModel>();
            PremiseViewModel premise = new PremiseViewModel();
            foreach (var item in servicePremises)
            {
                premise = new PremiseViewModel()
                {
                    Id = item.PremiseId,
                    AddressLine = item.Premise.AddressLine,
                    LocationName = item.Premise.LocationName,
                    PhoneNumber = item.Premise.PhoneNumber,
                    ProjectCode = item.ProjectCode
                };
                premisesViewModels.Add(premise);
            }

            return premisesViewModels;
        }

        public PagedResult<PremiseViewModel> GetPremiseWithPaging(int? page, int serviceId, bool includeInActive = false)
        {
            var query = premiseRepository.GetByServiceId(serviceId);

            if (!includeInActive)
                query = query.Where(x => x.IsActive == true);

            int rowCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)rowCount / Paging.PageSize);
            int currentPage = page.HasValue ? page.Value : 1;

            var premises = query.OrderBy(x => x.LocationName)
                            .Skip((currentPage - 1) * Paging.PageSize)
                            .Take(Paging.PageSize)
                            .ToList();

            var premisesViewModels = _mapper.Map<List<PremiseViewModel>>(query);
            int length = premisesViewModels.Count();

            for (int i = 0; i < length; i++)
            {
                premisesViewModels[i].ProjectCode = servicePremiseRepository.First(x => x.PremiseId == premisesViewModels[i].Id).ProjectCode;
            }

            return new PagedResult<PremiseViewModel>()
            {
                PageCount = pageCount,
                RowCount = rowCount,
                CurrentPage = currentPage,
                Items = premisesViewModels
            };
        }

        public bool RemoveServicePremise(int premiseId, int serviceId)
        {
            var servicePremise = servicePremiseRepository.First(x => x.PremiseId == premiseId && x.ServiceId == serviceId);
            if (servicePremise != null)
            {
                servicePremiseRepository.Remove(servicePremise);
                unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public int AddServicePremise(List<ServicePremiseViewModel> servicePremiseViews)
        {
            if (servicePremiseViews != null)
            {
                var servicePremise = _mapper.Map<List<ServicePremises>>(servicePremiseViews);
                servicePremiseRepository.AddRange(servicePremise);
                return unitOfWork.Commit();
            }
            return -1;
        }

        public List<PremiseViewModel> GetPremiseNotLink(int serviceId)
        {
            var premiseNotLinks = premiseRepository.GetAll(x => x.ServicePremises).ToList();
            var servicePremise = servicePremiseRepository.GetMany(x => x.ServiceId == serviceId).ToList();
            if (servicePremise != null)
            {
                int servicePremiseLength = servicePremise.Count();
                int premisesLength = premiseNotLinks.Count();
                for (int i = 0; i < servicePremiseLength; i++)
                {
                    for (int j = 0; j < premisesLength; j++)
                    {
                        if (servicePremise[i].PremiseId == premiseNotLinks[j].Id)
                        {
                            premiseNotLinks.Remove(premiseNotLinks[j]);
                            premisesLength--;
                        }
                    }
                }

                return _mapper.Map<List<PremiseViewModel>>(premiseNotLinks);
            }
            else
                return _mapper.Map<List<PremiseViewModel>>(premiseNotLinks);
        }
    }
}