using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System.Collections.Generic;

namespace ABSD.Application.Implements
{
    public class InterventionService : IInterventionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IInterventionRepository interventionRepository;

        public InterventionService(IUnitOfWork unitOfWork, IInterventionRepository interventionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.interventionRepository = interventionRepository;
        }

        public int Createintervention(InterventionViewModel interventionViewModel)
        {
            Intervention intervention = new Intervention()
            {
                Id = interventionViewModel.Id,
                InterventionName = interventionViewModel.InterventionName
            };
            interventionRepository.Add(intervention);

            return unitOfWork.Commit();
        }

        public List<InterventionViewModel> GetInterventionViewModel()
        {
            var query = interventionRepository.GetAll(x => x.ServiceInterventions);
            var interventionViewModelsList = new List<InterventionViewModel>();
            foreach (var item in query)
            {
                var interventionViewModel = new InterventionViewModel();
                interventionViewModel.Id = item.Id;
                interventionViewModel.InterventionName = item.InterventionName;
                //foreach (var i in item.ServiceInterventions)
                //{
                //    interventionViewModel.ServiceInterventions.Add(new ServiceInterventionViewModel()
                //    {
                //        InterventionId = i.InterventionId,
                //        ServiceId = i.ServiceId
                //    });
                //}
                interventionViewModelsList.Add(interventionViewModel);
            }

            return interventionViewModelsList;
        }

        public int Updateintervention(InterventionViewModel interventionViewModel)
        {
            Intervention intervention = interventionRepository.Single(x => x.Id == interventionViewModel.Id);
            intervention.InterventionName = interventionViewModel.InterventionName;
            interventionRepository.Update(intervention);

            return unitOfWork.Commit();
        }
    }
}