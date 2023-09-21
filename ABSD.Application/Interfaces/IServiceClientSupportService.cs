using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IServiceClientSupportService
    {
        List<ServiceClientSupportViewModel> GetServiceClientSupportViewModel();

        int CreateServiceClientSupport(ServiceViewModel serviceViewModel, List<ClientSupportViewModel> clientSupportViewModels);

        int UpdateServiceClientSupport(ServiceViewModel serviceViewModel, List<ClientSupportViewModel> clientSupportViewModels);
    }
}