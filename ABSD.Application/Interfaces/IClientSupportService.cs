using ABSD.Application.ViewModels;
using System.Collections.Generic;

namespace ABSD.Application.Interfaces
{
    public interface IClientSupportService
    {
        List<ClientSupportViewModel> GetClientSupportViewModel();

        int CreateClientSupport(ClientSupportViewModel clientSupportViewModel);

        int UpdateClientSupport(ClientSupportViewModel clientSupportViewModel);
    }
}