using ABSD.Application.Interfaces;
using ABSD.Application.ViewModels;
using ABSD.Data;
using ABSD.Data.Entities;
using ABSD.Data.IRepositories;
using System.Collections.Generic;

namespace ABSD.Application.Implements
{
    public class ParticipationService : IParticipationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IParticipationRepository participationRepository;

        public ParticipationService(IUnitOfWork unitOfWork, IParticipationRepository participationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.participationRepository = participationRepository;
        }

        public int CreateParticipation(ParticipationViewModel participationViewModel)
        {
            Participation participation = new Participation()
            {
                Id = participationViewModel.Id,
                ParticipationName = participationViewModel.ParticipationName,
            };

            participationRepository.Add(participation);

            return unitOfWork.Commit();
        }

        public List<ParticipationViewModel> GetParticipationViewModel()
        {
            var query = participationRepository.GetAll(x => x.ContractContents, x => x.Services);
            var participationViewModelsList = new List<ParticipationViewModel>();
            foreach (var item in query)
            {
                var participationViewModel = new ParticipationViewModel();
                participationViewModel.Id = item.Id;
                participationViewModel.ParticipationName = item.ParticipationName;
                participationViewModelsList.Add(participationViewModel);
            }

            return participationViewModelsList;
        }
    }
}

//public class ParticipationService : IParticipationService
//{
//    private readonly IUnitOfWork unitOfWork;
//    private readonly IParticipationRepository participationRepository;
//    private readonly IMapper mapper;
//    public ParticipationService(IUnitOfWork unitOfWork, IServiceTypeRepository serviceTypeRepository, IMapper mapper)
//    {
//        this.unitOfWork = unitOfWork;
//        this.participationRepository = participationRepository;
//        this.mapper = mapper;

//        return participationViewModelsList;
//    }

//    public int UpdateParticipation(ParticipationViewModel participationViewModel)
//    {
//        Participation participation = participationRepository.Single(x => x.Id == participationViewModel.Id);
//        participation.ParticipationName = participationViewModel.ParticipationName;

//        participationRepository.Update(participation);

//        return unitOfWork.Commit();
//    }
//}