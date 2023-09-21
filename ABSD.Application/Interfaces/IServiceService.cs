using ABSD.Application.ViewModels;
using ABSD.Common.Paging;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IServiceService
    {
		PagedResult<ServiceViewModel> GetServiceWithPaging(int? page, string firstCharacterToFilter = "", bool includeInActive = false);
		ServiceViewModel GetServiceDetail(int serviceId);
		int IsSeviceLink(int serviceId);
		int ActiveService(int serviceId);

		int InActiveService(int serviceId);

		int CreateService(ServiceViewModel serviceViewModel);

		int UpdateService(ServiceViewModel serviceViewModel);
		bool CheckExistedServiceName(string serviceName);
		ServiceViewModel CopyService(int serviceId);
		List<ServiceViewModel> GetAll();
		List<ServiceTypeViewModel> GetAllServiceTypes();
		List<ParticipationViewModel> GetParticipations();
		ServiceViewModel Coppy(int serviceId);

		PagedResult<ContactViewModel> GetContactWithPaging(int? page, string firstCharacterToFilter = "", bool includeInActive = false);
		ServiceViewModel GetService(int serviceId);
		ServiceViewModel GetLastestServiceViewModel();
		ContactViewModel GetContactById(int contactId);
	}
}
