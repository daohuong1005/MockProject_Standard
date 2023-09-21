using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System.Collections.Generic;

namespace ABSD.Application.Implements
{
    public class AccreditationService : IAccreditationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccreditationRepository accreditationRepository;

        public AccreditationService(IUnitOfWork unitOfWork, IAccreditationRepository accreditationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accreditationRepository = accreditationRepository;
        }

        public int CreateAccreditation(AccreditationViewModel accreditationViewModel)
        {
            Accreditation accreditation = new Accreditation()
            {
                Id = accreditationViewModel.Id,
                Name = accreditationViewModel.Name
            };
            accreditationRepository.Add(accreditation);

            return unitOfWork.Commit();
        }

        public List<AccreditationViewModel> GetAccreditationViewModel()
        {
            var query = accreditationRepository.GetAll(x => x.ServiceAccreditations);
            var accreditationViewModelsList = new List<AccreditationViewModel>();
            foreach (var item in query)
            {
                var accreditationViewModel = new AccreditationViewModel();
                accreditationViewModel.Id = item.Id;
                accreditationViewModel.Name = item.Name;
                foreach (var i in item.ServiceAccreditations)
                {
                    accreditationViewModel.serviceAccreditations.Add(new ServiceAccreditationViewModel()
                    {
                        ServiceId = i.ServiceId,
                        AccreditationId = i.AccreditationId
                    });
                }
                accreditationViewModelsList.Add(accreditationViewModel);
            }

            return accreditationViewModelsList;
        }

        public int UpdateAccreditationViewModel(AccreditationViewModel accreditationViewModel)
        {
            Accreditation accreditation = accreditationRepository.Single(x => x.Id == accreditationViewModel.Id);
            accreditation.Name = accreditationViewModel.Name;
            accreditationRepository.Update(accreditation);

            return unitOfWork.Commit();
        }
    }
}