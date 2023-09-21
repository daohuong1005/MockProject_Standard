using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.IRepositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABSD.Application.Implements
{
	public class ServiceTypeService : IServiceTypeService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IServiceTypeRepository serviceTypeRepository;
		private readonly IMapper mapper;
		public ServiceTypeService(IUnitOfWork unitOfWork, IServiceTypeRepository serviceTypeRepository, IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.serviceTypeRepository = serviceTypeRepository;
			this.mapper = mapper;
	
		}
		public List<ServiceTypeViewModel> GetServiceTypes()
		{
			var listServiceType = serviceTypeRepository.GetAll().ToList();
			List<ServiceTypeViewModel> serviceTypeViewModel = mapper.Map<List<ServiceTypeViewModel>>(listServiceType);
			return serviceTypeViewModel;
		}
	}
}
