using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Common.Constants;
using ABSD.Common.Dtos;
using ABSD.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ABSD.WebApp.Controllers
{
    public class ServiceController : Controller
    {
		private readonly IServiceService service;
		private readonly IParticipationService participation;
		private readonly IContentService content;
		private readonly IClientSupportService clientSupport;
		private readonly ICriterionService criterion;
		private readonly IInterventionService intervention;

		public ServiceController(IServiceService service, IParticipationService participation,
								 IClientSupportService clientSupport, ICriterionService criterion,
								 IContentService content, IInterventionService intervention)
		{
			this.service = service;
			this.content = content;
			this.participation = participation;
			this.criterion = criterion;
			this.clientSupport = clientSupport;
			this.intervention = intervention;
		}


		public IActionResult Index()
		{			
			return View();
		}
		#region AJAX API
		[HttpPost]
		public IActionResult Get(int? page, string firstCharacters = "", bool includeInActive = false)
		{
			try
			{
				var pagedResult = service.GetServiceWithPaging(page, firstCharacters, includeInActive);
				return Ok(new AjaxResult() {
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = pagedResult
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});

			}
		}

		[HttpPost]
		public IActionResult GetContacts(int? page, string firstCharacters = "", bool includeInActive = false)
		{
			try
			{
				var pagedResult = service.GetContactWithPaging(page, firstCharacters, includeInActive);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = pagedResult
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		#endregion
		[HttpPost]
		public IActionResult Active(int serviceId)
		{
			try
			{
				var pagedResult = service.ActiveService(serviceId);
				return Ok(new AjaxResult() {
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = pagedResult });
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult() {
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false });
			}
		}

		[HttpPost]
		public IActionResult CoppyService(int serviceId)
		{
			try
			{
				var result = service.Coppy(serviceId);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = result
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Success = false,
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage
				});
			}
		}

		[HttpPost]
		public IActionResult GetDetailService(int serviceId)
		{
			if (serviceId != 0)
			{
				ServiceViewModel serviceViewModel = service.GetServiceDetail(serviceId);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = serviceViewModel
				});
			}
			else
				return View();
		}

		[HttpPost]
		public IActionResult Copy(int serviceId)
		{
			if (serviceId != 0)
			{
				ServiceViewModel serviceViewModel = service.CopyService(serviceId);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = serviceViewModel
				});
			}
			else
				return View();
		}


		[HttpPost]
		public IActionResult InActive(int serviceId)
		{
			try
			{
				var pagedResult = service.InActiveService(serviceId);
				return Ok(new AjaxResult() {
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = pagedResult
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult() {
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		public IActionResult GetAllServiceTypes()
		{
			try
			{
				var list = service.GetAllServiceTypes();
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = list
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		[HttpPost]
		public IActionResult GetAllParticipations()
		{
			try
			{
				var list = service.GetParticipations();
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = list
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		[HttpPost]
		public IActionResult GetContactById(int contactId)
		{
			try
			{
				var contact = service.GetContactById(contactId);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = contact
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpGet]

		[HttpPost]
		public IActionResult GetService(int serviceId)
		{
			try
			{
				var result = service.GetService(serviceId);
				return Ok(new AjaxResult()
				{
					Success = true,
					Code = ReturnCode.OK,
					Data = result,
					ErrorMessage = string.Empty
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Success = false,
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage
				});
			}
		}

		[HttpPost]
		public IActionResult Create(ServiceViewModel serviceViewModel)
		{
			try
			{
				if (serviceViewModel.FundingId <= 0)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "Please select the Funding"
					});

				if (serviceViewModel.ParticipationId <= 0)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "Please select the Participation"
					});

				if (serviceViewModel.ServiceTypeId <= 0)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "Please select the ServiceType"
					});

				bool isExistedServiceName = service.CheckExistedServiceName(serviceViewModel.ServiceName);

				if (isExistedServiceName)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "The Service Name is existed. Please input the other",
					});

				
				service.CreateService(serviceViewModel);

				return Ok(new AjaxResult()
				{
					Success = true,
					Code = ReturnCode.OK,
					//Data = result,
					ErrorMessage = string.Empty,
				});
			}

			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Success = false,
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage
				});
			}
		}

		//[HttpPost]
		//public IActionResult UpdateService(ServiceViewModel serviceViewModel,int isIncludeTab)
		//{
		//	try
		//	{
		//		bool isExistedServiceName = service.CheckExistedServiceName(serviceViewModel.ServiceName);

		//		if (isExistedServiceName)
		//			return Ok(new AjaxResult()
		//			{
		//				Success = false,
		//				Code = ReturnCode.ValidationError,
		//				ErrorMessage = "The Service Name is existed. Please input the other",
		//			});

		//		var result = service.UpdateService(serviceViewModel, isIncludeTab);

		//		return Ok(new AjaxResult()
		//		{
		//			Success = true,
		//			Code = ReturnCode.OK,
		//			Data = result,
		//			ErrorMessage = string.Empty,
		//		});
		//	}

		//	catch (Exception ex)
		//	{
		//		return Ok(new AjaxResult()
		//		{
		//			Success = false,
		//			Code = ReturnCode.SystemError,
		//			ErrorMessage = ErrorMessage.SystemErrorMessage
		//		});
		//	}
		//}

		[HttpPost]
		public IActionResult Update(ServiceViewModel serviceViewModel)
		{
			try
			{
				if (serviceViewModel.ParticipationId <= 0)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "Please select the Participation"
					});

				if (serviceViewModel.ServiceTypeId <= 0)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "Please select the ServiceType"
					});

				bool isExistedServiceName = false;

				var currentService = service.GetServiceDetail(serviceViewModel.Id);
				if (currentService.ServiceName != serviceViewModel.ServiceName)
					isExistedServiceName = service.CheckExistedServiceName(serviceViewModel.ServiceName);

				if (isExistedServiceName)
					return Ok(new AjaxResult()
					{
						Success = false,
						Code = ReturnCode.ValidationError,
						ErrorMessage = "The Service is existed. Please input the other",
					});

				service.UpdateService(serviceViewModel);

				return Ok(new AjaxResult()
				{
					Success = true,
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty
				});
			}
			catch (Exception ex)
            {
                return Ok(new AjaxResult()
                {
                    Success = false,
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage
                });
			}
		}

		[HttpGet]
		public IActionResult GetParticipation()
		{
			try
			{
				var participationList = participation.GetParticipationViewModel();
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = participationList
				});
			}
			catch (Exception)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}
				

		[HttpGet]
		public IActionResult GetContent()
		{
			try
			{
				var contentList = new List<object>();
				var contentDetailList = content.GetContentViewModel();
				var contentTypeName = GeneralDictionary.Content.Values;
				contentList.Add(contentTypeName);
				contentList.Add(contentDetailList);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = contentList
				});
			}
			catch (Exception ex)
			{
                return Ok(new AjaxResult()
                {
                    Code = ReturnCode.SystemError,
                    ErrorMessage = ErrorMessage.SystemErrorMessage,
                    Success = false
                });
            }
		}

		[HttpGet]
		public IActionResult GetClientSupport()
		{
			try
			{
				var clientSupportList = new List<object>();
				var clientSupportDetailList = clientSupport.GetClientSupportViewModel();
				var clientSupportTypeName = GeneralDictionary.ClientSupporter.Values;
				clientSupportList.Add(clientSupportTypeName);
				clientSupportList.Add(clientSupportDetailList);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = clientSupportList
				});
			}
			catch (Exception)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		[HttpGet]
		public IActionResult GetCriterion()
		{
			try
			{
				var criterionList = new List<object>();
				var criterionDetailList = criterion.GetCriterionViewModel();
				var criterionTypeName = GeneralDictionary.Criterion.Values;
				criterionList.Add(criterionTypeName);
				criterionList.Add(criterionDetailList);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = criterionList
				});
			}
			catch (Exception)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		public IActionResult GetIntervention()
		{
			try
			{
				var interventionList = intervention.GetInterventionViewModel();
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = interventionList
				});
			}
			catch (Exception)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		[HttpPost]
		public IActionResult Details(int serviceId)
		{
			try
			{
				var result = service.GetServiceDetail(serviceId);
				return Ok(new AjaxResult()
				{
					Success = true,
					Code = ReturnCode.OK,
					Data = result,
					ErrorMessage = string.Empty
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}

		[HttpPost]
		public IActionResult IsSeviceLink(int serviceId)
		{
			try
			{
				var pagedResult = service.IsSeviceLink(serviceId);
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.OK,
					ErrorMessage = string.Empty,
					Success = true,
					Data = pagedResult
				});
			}
			catch (Exception ex)
			{
				return Ok(new AjaxResult()
				{
					Code = ReturnCode.SystemError,
					ErrorMessage = ErrorMessage.SystemErrorMessage,
					Success = false
				});
			}
		}
	}
}
