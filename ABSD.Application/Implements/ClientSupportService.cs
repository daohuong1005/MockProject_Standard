using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.Enums;
using ABSD.Data.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace ABSD.Application.Implements
{
    public class ClientSupportService : IClientSupportService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IClientSupportRepository clientSupportRepository;

        public ClientSupportService(IUnitOfWork unitOfWork, IClientSupportRepository clientSupportRepository)
        {
            this.unitOfWork = unitOfWork;
            this.clientSupportRepository = clientSupportRepository;
        }

        public int CreateClientSupport(ClientSupportViewModel clientSupportViewModel)
        {
            ClientSupport clientSupport = new ClientSupport()
            {
                Id = clientSupportViewModel.Id,
                Name = clientSupportViewModel.Name,
                Type = clientSupportViewModel.Type
            };
            clientSupportRepository.Add(clientSupport);

            return unitOfWork.Commit();
        }

        public List<ClientSupportViewModel> GetClientSupportViewModel()
        {
            var query = clientSupportRepository.GetAll(x => x.ServiceClientSupports);
            var clientSupportViewModelsList = new List<ClientSupportViewModel>();
            foreach (var item in query)
            {
                var clientSupportViewModel = new ClientSupportViewModel();
                clientSupportViewModel.Id = item.Id;
                clientSupportViewModel.Name = item.Name;
                clientSupportViewModel.Type = item.Type;
                clientSupportViewModel.TypeName = GeneralDictionary.ClientSupporter.Single(c => c.Key == item.Type).Value.ToString();
                //foreach (var i in item.ServiceClientSupports)
                //{
                //    clientSupportViewModel.ServiceClientSupport.Add(new ServiceClientSupportViewModel()
                //    {
                //        ClientSupportId = i.ClientSupportId,
                //        ServiceId = i.ServiceId
                //    });
                //}
                clientSupportViewModelsList.Add(clientSupportViewModel);
            }

            return clientSupportViewModelsList;
        }

        public int UpdateClientSupport(ClientSupportViewModel clientSupportViewModel)
        {
            ClientSupport clientSupport = clientSupportRepository.Single(x => x.Id == clientSupportViewModel.Id);
            clientSupport.Name = clientSupportViewModel.Name;
            clientSupport.Type = clientSupportViewModel.Type;
            clientSupportRepository.Update(clientSupport);

            return unitOfWork.Commit();
        }
    }
}