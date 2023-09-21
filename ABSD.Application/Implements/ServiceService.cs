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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IServiceRepository serviceRepository;
        private readonly IServiceTypeRepository serviceTypeRepository;
        private readonly IParticipationRepository participationRepository;
        private readonly IContactRepository contactRepository;
        private readonly IFundingRepository fundingRepository;
        private readonly IContentRepository contentRepository;
        private readonly IClientSupportRepository clientSupportRepository;
        private readonly ICriterionRepository criterionRepository;
        private readonly IInterventionRepository interventionRepository;
        private readonly IServiceClientSupportRepository serviceClientSupportRepository;
        private readonly IServiceCriterionSupportRepository serviceCriterionSupportRepository;
        private readonly IContractRepository contractRepository;
        private readonly IContractContentRepository contractContentRepository;
        private readonly IMapper mapper;

        public ServiceService(
            IUnitOfWork unitOfWork,
            IServiceRepository serviceRepository,
            IServiceTypeRepository serviceTypeRepository,
            IParticipationRepository participationRepository,
            IContactRepository contactRepository,
            IFundingRepository fundingRepository,
            IContentRepository contentRepository, IClientSupportRepository clientSupportRepository,
            ICriterionRepository criterionRepository, IInterventionRepository interventionRepository,
            IServiceClientSupportRepository serviceClientSupportRepository,
            IServiceCriterionSupportRepository serviceCriterionSupportRepository,
            IContractRepository contractRepository, IContractContentRepository contractContentRepository,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.serviceRepository = serviceRepository;
            this.serviceTypeRepository = serviceTypeRepository;
            this.participationRepository = participationRepository;
            this.contactRepository = contactRepository;
            this.fundingRepository = fundingRepository;
            this.contentRepository = contentRepository;
            this.clientSupportRepository = clientSupportRepository;
            this.criterionRepository = criterionRepository;
            this.interventionRepository = interventionRepository;
            this.clientSupportRepository = clientSupportRepository;
            this.criterionRepository = criterionRepository;
            this.contractRepository = contractRepository;
            this.contractContentRepository = contractContentRepository;
            this.serviceClientSupportRepository = serviceClientSupportRepository;
            this.serviceCriterionSupportRepository = serviceCriterionSupportRepository;
            this.mapper = mapper;
        }

        public PagedResult<ServiceViewModel> GetServiceWithPaging(int? page, string firstCharacterToFilter = "", bool includeInActive = false)
        {
            var query = serviceRepository.GetAll(x => x.ServiceType, x => x.Contact);
            if (!includeInActive)
            {
                query = query.Where(x => x.ServiceActive == true);
            }
            if (!string.IsNullOrEmpty(firstCharacterToFilter))
            {
                query = query.Where(x => firstCharacterToFilter.Contains(x.ServiceName.Substring(0, 1)));
            }
            int rowCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)rowCount / Paging.PageSize);
            int currentPage = page.HasValue ? page.Value : 1;

			var services = query.OrderBy(x => x.ServiceName)
							.Skip((currentPage - 1) * Paging.PageSize)
							.Take(currentPage * Paging.PageSize)
							.ToList();
			var serviceViewModel = new List<ServiceViewModel>();

			foreach (var item in services)
			{
				serviceViewModel.Add(new ServiceViewModel()
				{
					Id = item.Id,
					ServiceName = item.ServiceName,
					ServiceShortDescription = item.ServiceShortDescription,
					ServiceTypeViewModel = new ServiceTypeViewModel()
					{

						Id = item.ServiceType.Id,
						Name = item.ServiceType.Name
					},
					ContactViewModel = new ContactViewModel()
					{
						Id = item.Contact.Id,
						FirstName = item.Contact.FirstName,
						SurName = item.Contact.SurName
					},
					ServiceActive = item.ServiceActive
				});
			}

			return new PagedResult<ServiceViewModel>()
			{
				PageCount = pageCount,
				RowCount = rowCount,
				CurrentPage = currentPage,
				Items = serviceViewModel
			};
		}

		public int ActiveService(int serviceId)
		{
			var service = serviceRepository.Single(x => x.Id == serviceId);

			if (service != null)
			{
				service.ServiceActive = true;

				return unitOfWork.Commit();
			}

			return 0;
		}

		public int CreateService(ServiceViewModel serviceViewModel)
		{
			using (var transaction = unitOfWork.Context.Database.BeginTransaction())
			{
				try
				{
					var contractContentList = new List<ContractContent>();
					var serviceClientSupportList = new List<ServiceClientSupport>();
					var serviceCriterionSupportList = new List<ServiceCriterionSupport>();
					var criterionViewModels = serviceViewModel.CriterionViewModel;
					var clientSupportViewModels = serviceViewModel.ClientSupportViewModels;
					var contentViewModels = serviceViewModel.ContentViewModels;
					var participationViewModel = serviceViewModel.ParticipationViewModel;
					int lastContractId = 0;

					Service service = new Service();
					service.ServiceName = serviceViewModel.ServiceName;
					service.ServiceShortDescription = serviceViewModel.ServiceShortDescription;
					service.ContactId = serviceViewModel.ContactId;
					service.ClientDescription = serviceViewModel.ClientDescription;
					service.ParticipationId = serviceViewModel.ParticipationId;
					service.ServiceStartExpected = serviceViewModel.ServiceStartExpected;
					service.ServiceStartDate = serviceViewModel.ServiceStartDate;
					service.ServiceEndDate = serviceViewModel.ServiceEndDate;
					service.ServiceExtendable = serviceViewModel.ServiceExtendable;
					service.ServiceActive = true;
					service.ServiceFullDescription = serviceViewModel.ServiceFullDescription;
					service.DeptCode = serviceViewModel.DeptCode;
					service.ServiceTypeId = serviceViewModel.ServiceTypeId;
					service.ServiceDesscriptionDelivery = serviceViewModel.ServiceDesscriptionDelivery;
					service.ServiceContactCode = serviceViewModel.ServiceContactCode;
					service.ServiceContractValue = serviceViewModel.ServiceContractValue;
					service.ContractStagedPayment = serviceViewModel.ContractStagedPayment;
					service.ReferralProcess = serviceViewModel.ReferralProcess;
					service.ServiceTimeLimited = serviceViewModel.ServiceTimeLimited;

					if (contentViewModels != null)
					{
						Contract contract = new Contract()
						{
							ContractName = serviceViewModel.ServiceName + " Contract"
						};

						contractRepository.Add(contract);
						unitOfWork.Commit();

						lastContractId = contractRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault().Id;

						foreach (var item in contentViewModels)
						{
							contractContentList.Add(new ContractContent()
							{
								ContractId = lastContractId,
								ContentId = item.Id,
								ParticipationId = participationViewModel.Id
							});
						}
						contractContentRepository.AddRange(contractContentList);

						service.ContractId = lastContractId;
					}
					else
					{
						Funding funding = new Funding()
						{
							FundingSource = serviceViewModel.FundingViewModel.FundingSource,
							ContactId = serviceViewModel.FundingViewModel.ContactId,
							FundingAmount = serviceViewModel.FundingViewModel.FundingAmount,
							FundingStart = serviceViewModel.FundingViewModel.FundingStart,
							FundingEnd = serviceViewModel.FundingViewModel.FundingEnd,
							FundraisingForText = serviceViewModel.FundingViewModel.FundraisingForText,
							FundraisingWhy = serviceViewModel.FundingViewModel.FundraisingWhy,
							FundraisingDonorAnonymous = serviceViewModel.FundingViewModel.FundraisingDonorAnonymous,
							FundraisingDonorAmount = serviceViewModel.FundingViewModel.FundraisingDonorAmount,
							FundingNeeds = serviceViewModel.FundingViewModel.FundingNeeds,
							FundingContinuationNeeded = serviceViewModel.FundingViewModel.FundingContinuationNeeded,
							FundingContinuationAmount = serviceViewModel.FundingViewModel.FundingContinuationAmount,
							FundingContinuationDetails = serviceViewModel.FundingViewModel.FundingContinuationDetails,
							FundraisingNeeded = serviceViewModel.FundingViewModel.FundraisingNeeded,
							FundraisingRequiredBy = serviceViewModel.FundingViewModel.FundraisingRequiredBy,
							FundraisingComplete = serviceViewModel.FundingViewModel.FundraisingComplete,
							FundraisingCompleteDate = serviceViewModel.FundingViewModel.FundraisingCompleteDate,
							FundraisingDonationDate = serviceViewModel.FundingViewModel.FundraisingDonationDate,
							FundraisingDonationIncremental = serviceViewModel.FundingViewModel.FundraisingDonationIncremental

						};

						fundingRepository.Add(funding);
						unitOfWork.Commit();

						service.FundingId = funding.Id;
					}

					serviceRepository.Add(service);
					unitOfWork.Commit();

					var lastService = serviceRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault();
					foreach (var item in clientSupportViewModels)
					{
						serviceClientSupportList.Add(new ServiceClientSupport()
						{
							ClientSupportId = item.Id,
							ServiceId = lastService.Id
						});
					}
					serviceClientSupportRepository.AddRange(serviceClientSupportList);
					unitOfWork.Commit();

					foreach (var item in criterionViewModels)
					{
						serviceCriterionSupportList.Add(new ServiceCriterionSupport()
						{
							CriterionId = item.Id,
							ServiceId = lastService.Id
						});
					}
					serviceCriterionSupportRepository.AddRange(serviceCriterionSupportList);
					unitOfWork.Commit();

					transaction.Commit();

					return 1;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw;
				}
			}
		}

		public int InActiveService(int serviceId)
		{
			var service = serviceRepository.Single(x => x.Id == serviceId);

			if (service != null)
			{
				if (service.ServiceOrganizations == null)
				{
					service.ServiceActive = false;
					unitOfWork.Commit();
					return 1;
				}
				else if (service.ServicePremises == null)
				{
					service.ServiceActive = false;
					unitOfWork.Commit();
					return 2;
				}
				else
				{
					service.ServiceActive = false;
					unitOfWork.Commit();
					return 0;
				}
			}

			return -1;
		}

		public ServiceViewModel Coppy(int serviceId)
		{

			Service service = serviceRepository.Single(x => x.Id == serviceId, x => x.Funding, x => x.ServiceType, x => x.Participation);
			if (service == null)
				return null;

            ServiceViewModel serviceViewModel = mapper.Map<ServiceViewModel>(service);
            serviceViewModel.ServiceName = "";
            var fundingViewModel = fundingRepository.Single(x => x.Id == serviceViewModel.FundingId);
            FundingViewModel funding = new FundingViewModel
            {
                Id = fundingViewModel.Id,
                FundingSource = fundingViewModel.FundingSource,
                ContactId = fundingViewModel.ContactId,
                FundingAmount = fundingViewModel.FundingAmount,
                FundingStart = fundingViewModel.FundingStart,
                FundingEnd = fundingViewModel.FundingEnd,
                FundraisingForText = fundingViewModel.FundraisingForText,
                FundraisingWhy = fundingViewModel.FundraisingWhy,
                FundraisingDonorAnonymous = fundingViewModel.FundraisingDonorAnonymous,
                FundraisingDonorAmount = fundingViewModel.FundraisingDonorAmount,
                FundingNeeds = fundingViewModel.FundingNeeds,
                FundingContinuationNeeded = fundingViewModel.FundingContinuationNeeded,
                FundingContinuationAmount = fundingViewModel.FundingContinuationAmount,
                FundingContinuationDetails = fundingViewModel.FundingContinuationDetails,
                FundraisingNeeded = fundingViewModel.FundraisingNeeded,
                FundraisingRequiredBy = fundingViewModel.FundraisingRequiredBy,
                FundraisingComplete = fundingViewModel.FundraisingComplete,
                FundraisingCompleteDate = fundingViewModel.FundraisingCompleteDate,
                FundraisingDonationDate = fundingViewModel.FundraisingDonationDate,
                FundraisingDonationIncremental = fundingViewModel.FundraisingDonationIncremental
            };
            serviceViewModel.FundingViewModel = funding;
            return serviceViewModel;
        }

        public ServiceViewModel GetServiceDetail(int serviceId)
        {
            Service service = serviceRepository.Single(x => x.Id == serviceId, x => x.ServiceType, x => x.Participation, x => x.Funding);
            if (service == null)
                return null;

			List<CriterionViewModel> criterionViewModels = new List<CriterionViewModel>();
			List<ContentViewModel> contentViewModels = new List<ContentViewModel>();
			List<ClientSupportViewModel> clientSupportViewModels = new List<ClientSupportViewModel>();

			ServiceViewModel serviceViewModel = mapper.Map<ServiceViewModel>(service);
			var ServiceType = serviceTypeRepository.First(x => x.Id == service.ServiceTypeId);
			var Contact = contactRepository.First(x => x.Id == service.ContactId);
			var Participation = participationRepository.First(x => x.Id == service.ParticipationId);
			var fundingViewModel = fundingRepository.Single(x => x.Id == serviceViewModel.FundingId);

			ServiceTypeViewModel serviceTypeViewModel = mapper.Map<ServiceTypeViewModel>(ServiceType);
			ContactViewModel contactViewModel = mapper.Map<ContactViewModel>(Contact);
			ParticipationViewModel participationViewModel = mapper.Map<ParticipationViewModel>(Participation);

			if (fundingViewModel != null)
			{
				FundingViewModel funding = new FundingViewModel
				{
					Id = fundingViewModel.Id,
					FundingSource = fundingViewModel.FundingSource,
					ContactId = fundingViewModel.ContactId,
					FundingAmount = fundingViewModel.FundingAmount,
					FundingStart = fundingViewModel.FundingStart,
					FundingEnd = fundingViewModel.FundingEnd,
					FundraisingForText = fundingViewModel.FundraisingForText,
					FundraisingWhy = fundingViewModel.FundraisingWhy,
					FundraisingDonorAnonymous = fundingViewModel.FundraisingDonorAnonymous,
					FundraisingDonorAmount = fundingViewModel.FundraisingDonorAmount,
					FundingNeeds = fundingViewModel.FundingNeeds,
					FundingContinuationNeeded = fundingViewModel.FundingContinuationNeeded,
					FundingContinuationAmount = fundingViewModel.FundingContinuationAmount,
					FundingContinuationDetails = fundingViewModel.FundingContinuationDetails,
					FundraisingNeeded = fundingViewModel.FundraisingNeeded,
					FundraisingRequiredBy = fundingViewModel.FundraisingRequiredBy,
					FundraisingComplete = fundingViewModel.FundraisingComplete,
					FundraisingCompleteDate = fundingViewModel.FundraisingCompleteDate,
					FundraisingDonationDate = fundingViewModel.FundraisingDonationDate,
					FundraisingDonationIncremental = fundingViewModel.FundraisingDonationIncremental

				};
				serviceViewModel.FundingViewModel = funding;
			}


			var queryServiceClientSupport = serviceClientSupportRepository.GetMany(x => x.ServiceId == serviceId).ToList();
			foreach (var item in queryServiceClientSupport)
			{
				var queryClientSupport = clientSupportRepository.Single(x => x.Id == item.ClientSupportId);
				clientSupportViewModels.Add(new ClientSupportViewModel()
				{
					Id = queryClientSupport.Id,
					Name = queryClientSupport.Name,
				});
			}

			var queryServiceCriterionSupport = serviceCriterionSupportRepository.GetMany(x => x.ServiceId == serviceId).ToList();
			foreach (var item in queryServiceCriterionSupport)
			{
				var queryCriterionSupport = criterionRepository.Single(x => x.Id == item.CriterionId);
				criterionViewModels.Add(new CriterionViewModel()
				{
					Id = queryCriterionSupport.Id,
					Name = queryCriterionSupport.Name
				});
			}
			var queryContractContent = contractContentRepository.GetMany(x => x.ContractId == serviceViewModel.ContractId).ToList();

			if (queryContractContent.Count > 0)
			{
				int participationId = 0;
				foreach (var item in queryContractContent)
				{
					var queryContent = contentRepository.Single(x => x.Id == item.ContentId);
					contentViewModels.Add(new ContentViewModel()
					{
						Id = queryContent.Id,
						ContentName = queryContent.ContentName
					});
					participationId = item.ParticipationId;
				}

				var participation = participationRepository.First(x => x.Id == participationId);

				serviceViewModel.ParticipationViewModel = new ParticipationViewModel()
				{
					Id = participation.Id,
					ParticipationName = participation.ParticipationName
				};
				serviceViewModel.ContentViewModels = contentViewModels;
			}
			serviceViewModel.ClientSupportViewModels = clientSupportViewModels;
			serviceViewModel.CriterionViewModel = criterionViewModels;

			return serviceViewModel;
		}

		public ServiceViewModel CopyService(int serviceId)
		{
			Service service = serviceRepository.Single(x => x.Id == serviceId, x => x.ServiceType, x => x.Participation, x => x.Funding);
			if (service == null)
				return null;

			List<CriterionViewModel> criterionViewModels = new List<CriterionViewModel>();
			List<ContentViewModel> contentViewModels = new List<ContentViewModel>();
			List<ClientSupportViewModel> clientSupportViewModels = new List<ClientSupportViewModel>();

			ServiceViewModel serviceViewModel = mapper.Map<ServiceViewModel>(service);
			serviceViewModel.ServiceName = "";
			//var funding = fundingRepository.First(x => x.Id == service.FundingId);
			var ServiceType = serviceTypeRepository.First(x => x.Id == service.ServiceTypeId);
			var Contact = contactRepository.First(x => x.Id == service.ContactId);
			var Participation = participationRepository.First(x => x.Id == service.ParticipationId);
			var fundingViewModel = fundingRepository.Single(x => x.Id == serviceViewModel.FundingId);

			ServiceTypeViewModel serviceTypeViewModel = mapper.Map<ServiceTypeViewModel>(ServiceType);
			ContactViewModel contactViewModel = mapper.Map<ContactViewModel>(Contact);
			ParticipationViewModel participationViewModel = mapper.Map<ParticipationViewModel>(Participation);

			if(fundingViewModel != null)
            {
				FundingViewModel funding = new FundingViewModel
				{
					Id = fundingViewModel.Id,
					FundingSource = fundingViewModel.FundingSource,
					ContactId = fundingViewModel.ContactId,
					FundingAmount = fundingViewModel.FundingAmount,
					FundingStart = fundingViewModel.FundingStart,
					FundingEnd = fundingViewModel.FundingEnd,
					FundraisingForText = fundingViewModel.FundraisingForText,
					FundraisingWhy = fundingViewModel.FundraisingWhy,
					FundraisingDonorAnonymous = fundingViewModel.FundraisingDonorAnonymous,
					FundraisingDonorAmount = fundingViewModel.FundraisingDonorAmount,
					FundingNeeds = fundingViewModel.FundingNeeds,
					FundingContinuationNeeded = fundingViewModel.FundingContinuationNeeded,
					FundingContinuationAmount = fundingViewModel.FundingContinuationAmount,
					FundingContinuationDetails = fundingViewModel.FundingContinuationDetails,
					FundraisingNeeded = fundingViewModel.FundraisingNeeded,
					FundraisingRequiredBy = fundingViewModel.FundraisingRequiredBy,
					FundraisingComplete = fundingViewModel.FundraisingComplete,
					FundraisingCompleteDate = fundingViewModel.FundraisingCompleteDate,
					FundraisingDonationDate = fundingViewModel.FundraisingDonationDate,
					FundraisingDonationIncremental = fundingViewModel.FundraisingDonationIncremental

				};
				serviceViewModel.FundingViewModel = funding;
			}

			var queryServiceClientSupport = serviceClientSupportRepository.GetMany(x => x.ServiceId == serviceId).ToList();
			foreach (var item in queryServiceClientSupport)
			{
				var queryClientSupport = clientSupportRepository.Single(x => x.Id == item.ClientSupportId);
				clientSupportViewModels.Add(new ClientSupportViewModel()
				{
					Id = queryClientSupport.Id,
					Name = queryClientSupport.Name,
				});
			}

			var queryServiceCriterionSupport = serviceCriterionSupportRepository.GetMany(x => x.ServiceId == serviceId).ToList();
			foreach (var item in queryServiceCriterionSupport)
			{
				var queryCriterionSupport = criterionRepository.Single(x => x.Id == item.CriterionId);
				criterionViewModels.Add(new CriterionViewModel()
				{
					Id = queryCriterionSupport.Id,
					Name = queryCriterionSupport.Name
				});
			}

			var queryContractContent = contractContentRepository.GetMany(x => x.ContractId == serviceViewModel.ContractId).ToList();
			if(queryContractContent.Count > 0)
            {
				foreach (var item in queryContractContent)
				{
					var queryContent = contentRepository.Single(x => x.Id == item.ContentId);
					contentViewModels.Add(new ContentViewModel()
					{
						Id = queryContent.Id,
						ContentName = queryContent.ContentName
					});
				}

				serviceViewModel.ContentViewModels = contentViewModels;
			}


			serviceViewModel.ClientSupportViewModels = clientSupportViewModels;
			serviceViewModel.CriterionViewModel = criterionViewModels;

			return serviceViewModel;

		}

		public List<ServiceViewModel> GetAll()

		{

			var service = serviceRepository.GetAll().ToList();
			List<ServiceViewModel> serviceViewModel = mapper.Map<List<ServiceViewModel>>(service);
			if (service.Count == 0)
				return serviceViewModel;

			return serviceViewModel;

		}

		public bool CheckExistedServiceName(string serviceName)
		{
			return serviceRepository.GetMany(x => x.ServiceName.ToLower() == serviceName.ToLower())
								   .Count() > 0;
		}

		public List<ServiceTypeViewModel> GetAllServiceTypes()
		{
			var listServiceType = serviceTypeRepository.GetAll().ToList();
			List<ServiceTypeViewModel> serviceTypeViewModel = mapper.Map<List<ServiceTypeViewModel>>(listServiceType);
			return serviceTypeViewModel;
		}

		public ServiceViewModel GetService(int serviceId)
		{
			Service service = serviceRepository.Single(x => x.Id == serviceId, x => x.ServiceType, x => x.Participation, x => x.Funding);
			if (service == null)
				return null;

			ServiceViewModel serviceViewModel = mapper.Map<ServiceViewModel>(service);
			return serviceViewModel;
		}

		public List<ParticipationViewModel> GetParticipations()
		{
			var listParticipation = participationRepository.GetAll().ToList();
			List<ParticipationViewModel> participationViewModels = mapper.Map<List<ParticipationViewModel>>(listParticipation);
			return participationViewModels;
		}

		public PagedResult<ContactViewModel> GetContactWithPaging(int? page, string firstCharacterToFilter = "", bool includeInActive = false)
		{
			var query = contactRepository.GetAll();
			if (!includeInActive)
			{
				query.Where(x => x.IsActive == true);
			}
			if (!string.IsNullOrEmpty(firstCharacterToFilter))
			{
				query = query.Where(x => firstCharacterToFilter.Contains(x.FirstName.Substring(0, 1)));

			}
			int rowCount = query.Count();
			int pageCount = (int)Math.Ceiling((double)rowCount / Paging.PageSize);
			int currentPage = page.HasValue ? page.Value : 1;

			var contacts = query.OrderBy(x => x.FirstName)
							.Skip((currentPage - 1) * Paging.PageSize)
							.Take(currentPage * Paging.PageSize)
							.ToList();
			var contactViewModel = new List<ContactViewModel>();

			foreach (var item in contacts)
			{
				contactViewModel.Add(new ContactViewModel()
				{
					Id = item.Id,
					FirstName = item.FirstName,
					SurName = item.SurName,
					MobilePhone = item.MobilePhone,
					Email = item.Email,
					ContactType = item.ContactType,
					IsActive = item.IsActive
				});
			}

			return new PagedResult<ContactViewModel>()
			{
				PageCount = pageCount,
				RowCount = rowCount,
				CurrentPage = currentPage,
				Items = contactViewModel
			};
		}

        public ServiceViewModel GetLastestServiceViewModel()
        {
            var lastestService = serviceRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            var lastestServiceViewModel = new ServiceViewModel()
            {
                Id = lastestService.Id,
                ServiceName = lastestService.ServiceName,
                ServiceShortDescription = lastestService.ServiceShortDescription,
                FundingId = (int)lastestService.FundingId,
                ContactId = lastestService.ContactId,
                ContractId = (int)lastestService.ContractId,
                ClientDescription = lastestService.ClientDescription,
                ParticipationId = (int)lastestService.ParticipationId,
                ServiceStartExpected = lastestService.ServiceStartExpected,
                ServiceStartDate = lastestService.ServiceStartDate,
                ServiceEndDate = lastestService.ServiceEndDate,
                ServiceExtendable = lastestService.ServiceExtendable,
                ServiceActive = true,
                ServiceFullDescription = lastestService.ServiceFullDescription,
                DeptCode = lastestService.DeptCode,
                ServiceTypeId = lastestService.ServiceTypeId,
                ServiceDesscriptionDelivery = lastestService.ServiceDesscriptionDelivery,
                ServiceContactCode = lastestService.ServiceContactCode,
                ServiceContractValue = lastestService.ServiceContractValue,
                ContractStagedPayment = lastestService.ContractStagedPayment,
                ReferralProcess = lastestService.ReferralProcess,
                ServiceTimeLimited = lastestService.ServiceTimeLimited
            };

			return lastestServiceViewModel;
		}

		public ContactViewModel GetContactById(int contactId)
		{
			var contact = contactRepository.Single(x => x.Id == contactId);
			if (contact == null)
			{
				return null;
			}
			ContactViewModel contactViewModel = new ContactViewModel
			{
				Id = contact.Id,
				FirstName = contact.FirstName,
				SurName = contact.SurName
			};

			return contactViewModel;

		}

		public int UpdateService(ServiceViewModel serviceViewModel)
		{
			using (var transaction = unitOfWork.Context.Database.BeginTransaction())
			{
				try
				{
					Service service = serviceRepository.Single(x => x.Id == serviceViewModel.Id, x => x.Funding, x => x.ServiceType, x => x.Participation);

					service.ServiceName = serviceViewModel.ServiceName;
					service.ServiceShortDescription = serviceViewModel.ServiceShortDescription;
					service.FundingId = serviceViewModel.FundingId;
					service.ContactId = serviceViewModel.ContactId;
					service.ClientDescription = serviceViewModel.ClientDescription;
					service.ParticipationId = serviceViewModel.ParticipationId;
					service.ServiceStartExpected = serviceViewModel.ServiceStartExpected;
					service.ServiceStartDate = serviceViewModel.ServiceStartDate;
					service.ServiceEndDate = serviceViewModel.ServiceEndDate;
					service.ServiceExtendable = serviceViewModel.ServiceExtendable;
					service.ServiceActive = serviceViewModel.ServiceActive;
					service.ServiceFullDescription = serviceViewModel.ServiceFullDescription;
					service.DeptCode = serviceViewModel.DeptCode;
					service.ServiceTypeId = serviceViewModel.ServiceTypeId;
					service.ServiceDesscriptionDelivery = serviceViewModel.ServiceDesscriptionDelivery;
					service.ServiceContactCode = serviceViewModel.ServiceContactCode;
					service.ServiceContractValue = serviceViewModel.ServiceContractValue;
					service.ContractStagedPayment = serviceViewModel.ContractStagedPayment;
					service.ReferralProcess = serviceViewModel.ReferralProcess;
					service.ServiceTimeLimited = serviceViewModel.ServiceTimeLimited;

					var contractContentList = new List<ContractContent>();
					var serviceClientSupportList = new List<ServiceClientSupport>();
					var serviceCriterionSupportList = new List<ServiceCriterionSupport>();
					var criterionViewModels = serviceViewModel.CriterionViewModel;
					var clientSupportViewModels = serviceViewModel.ClientSupportViewModels;
					var contentViewModels = serviceViewModel.ContentViewModels;
					var participationViewModel = serviceViewModel.ParticipationViewModel;

					if (contentViewModels != null)
					{
						var contractContent = contractContentRepository.GetMany(x => x.ContractId == service.ContractId).ToList();
						if (contentViewModels != null && contractContent.Count > 0)
						{
							contractContentRepository.RemoveRange(contractContent);
							foreach (var item in contentViewModels)
							{
								var newContractContent = new ContractContent();
								newContractContent.ContractId = (int)service.ContractId;
								newContractContent.ContentId = item.Id;
								newContractContent.ParticipationId = participationViewModel.Id;
								contractContentList.Add(newContractContent);
							}
							contractContentRepository.AddRange(contractContentList);
							unitOfWork.Commit();
						}
						else if (contentViewModels != null && contractContent.Count == 0)
						{
							Contract contract = new Contract()
							{
								ContractName = serviceViewModel.ServiceName + " Contract"
							};

							contractRepository.Add(contract);
							unitOfWork.Commit();
							var lastContract = contractRepository.GetAll().OrderByDescending(o => o.Id).FirstOrDefault();

							foreach (var item in contentViewModels)
							{
								var newContractContent = new ContractContent();
								newContractContent.ContractId = lastContract.Id;
								newContractContent.ContentId = item.Id;
								newContractContent.ParticipationId = (int)participationViewModel.Id;
								contractContentList.Add(newContractContent);
							}
							contractContentRepository.AddRange(contractContentList);
							unitOfWork.Commit();

							var query = contractRepository.Single(x => x.Id == service.ContractId);
							if (query != null)
								contractRepository.Remove(query);
							service.ContractId = lastContract.Id;
						}
						else
							contractContentRepository.RemoveRange(contractContent);
					}

					else if (serviceViewModel.FundingViewModel != null)
					{
						var query = fundingRepository.Single(x => x.Id == serviceViewModel.FundingId);
						var fundingViewModel = query;
						fundingViewModel.FundingSource = serviceViewModel.FundingViewModel.FundingSource;
						fundingViewModel.ContactId = serviceViewModel.FundingViewModel.ContactId;
						fundingViewModel.FundingAmount = serviceViewModel.FundingViewModel.FundingAmount;
						fundingViewModel.FundingStart = serviceViewModel.FundingViewModel.FundingStart;
						fundingViewModel.FundingEnd = serviceViewModel.FundingViewModel.FundingEnd;
						fundingViewModel.FundraisingForText = serviceViewModel.FundingViewModel.FundraisingForText;
						fundingViewModel.FundraisingWhy = serviceViewModel.FundingViewModel.FundraisingWhy;
						fundingViewModel.FundraisingDonorAnonymous = serviceViewModel.FundingViewModel.FundraisingDonorAnonymous;
						fundingViewModel.FundraisingDonorAmount = serviceViewModel.FundingViewModel.FundraisingDonorAmount;
						fundingViewModel.FundingNeeds = serviceViewModel.FundingViewModel.FundingNeeds;
						fundingViewModel.FundingContinuationNeeded = serviceViewModel.FundingViewModel.FundingContinuationNeeded;
						fundingViewModel.FundingContinuationAmount = serviceViewModel.FundingViewModel.FundingContinuationAmount;
						fundingViewModel.FundingContinuationDetails = serviceViewModel.FundingViewModel.FundingContinuationDetails;
						fundingViewModel.FundraisingNeeded = serviceViewModel.FundingViewModel.FundraisingNeeded;
						fundingViewModel.FundraisingRequiredBy = serviceViewModel.FundingViewModel.FundraisingRequiredBy;
						fundingViewModel.FundraisingComplete = serviceViewModel.FundingViewModel.FundraisingComplete;
						fundingViewModel.FundraisingCompleteDate = serviceViewModel.FundingViewModel.FundraisingCompleteDate;
						fundingViewModel.FundraisingDonationDate = serviceViewModel.FundingViewModel.FundraisingDonationDate;
						fundingViewModel.FundraisingDonationIncremental = serviceViewModel.FundingViewModel.FundraisingDonationIncremental;

						if (query != null)
							fundingRepository.Update(fundingViewModel);
						else if (query == null)
						{
							fundingRepository.Add(fundingViewModel);
							service.FundingId = service.Funding.Id;
						}

						unitOfWork.Commit();
					}

					serviceRepository.Update(service);
					unitOfWork.Commit();

					var serviceClientSupport = serviceClientSupportRepository.GetMany(x => x.ServiceId == serviceViewModel.Id).ToList();
					serviceClientSupportRepository.RemoveRange(serviceClientSupport);
					if (clientSupportViewModels != null)
					{
						foreach (var item in clientSupportViewModels)
						{
							serviceClientSupportList.Add(new ServiceClientSupport()
							{
								ClientSupportId = item.Id,
								ServiceId = service.Id
							});
						}
						serviceClientSupportRepository.AddRange(serviceClientSupportList);
						unitOfWork.Commit();
					}

					var serviceCriterionSupport = serviceCriterionSupportRepository.GetMany(x => x.ServiceId == serviceViewModel.Id).ToList();
					serviceCriterionSupportRepository.RemoveRange(serviceCriterionSupport);
					if (criterionViewModels != null)
					{
						foreach (var item in criterionViewModels)
						{
							serviceCriterionSupportList.Add(new ServiceCriterionSupport()
							{
								CriterionId = item.Id,
								ServiceId = service.Id
							});
						}
						serviceCriterionSupportRepository.AddRange(serviceCriterionSupportList);
						unitOfWork.Commit();
					}

					transaction.Commit();

					return 1;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw ex;
				}
			}


		}

		public int IsSeviceLink(int serviceId)
		{
			var service = serviceRepository.Single(x => x.Id == serviceId, x => x.ServiceOrganizations, x => x.ServicePremises);
			if (service != null)
			{
				if (service.ServiceOrganizations.Count() != 0 || service.ServicePremises.Count() != 0)
					return 1;
				else
					return 0;
			}

			return -1;
		}
	}
}