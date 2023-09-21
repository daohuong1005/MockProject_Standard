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
    public class CriterionService : ICriterionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICriterionRepository criterionRepository;

        public CriterionService(IUnitOfWork unitOfWork, ICriterionRepository criterionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.criterionRepository = criterionRepository;
        }

        public int CreateCriterion(CriterionViewModel criterionViewModel)
        {
            Criterion criterion = new Criterion()
            {
                Id = criterionViewModel.Id,
                Name = criterionViewModel.Name,
                Type = criterionViewModel.Type
            };
            criterionRepository.Add(criterion);

            return unitOfWork.Commit();
        }

        public List<CriterionViewModel> GetCriterionViewModel()
        {
            var query = criterionRepository.GetAll().ToList();
            var criterionViewModelsList = new List<CriterionViewModel>();
            foreach (var item in query)
            {
                var criterionViewModel = new CriterionViewModel();
                criterionViewModel.Id = item.Id;
                criterionViewModel.Name = item.Name;
                criterionViewModel.TypeName = GeneralDictionary.Criterion.Single(c => c.Key == item.Type).Value.ToString();
                //foreach (var i in item.ServiceCriterionSupports)
                //{
                //    criterionViewModel.ServiceCriterionSupports.Add(new ServiceCriterionSupportViewModel()
                //    {
                //        CriterionId = i.CriterionId,
                //        ServiceId = i.ServiceId
                //    });
                //}
                criterionViewModelsList.Add(criterionViewModel);
            }

            return criterionViewModelsList;
        }

        public int UpdateCriterion(CriterionViewModel criterionViewModel)
        {
            Criterion criterion = criterionRepository.Single(x => x.Id == criterionViewModel.Id);
            criterion.Name = criterionViewModel.Name;
            criterion.Type = criterionViewModel.Type;
            criterionRepository.Update(criterion);

            return unitOfWork.Commit();
        }
    }
}