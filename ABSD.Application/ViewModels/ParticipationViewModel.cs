using System.Collections.Generic;

namespace ABSD.Application.ViewModels
{
    public class ParticipationViewModel
    {
        public int Id { get; set; }
        public string ParticipationName { get; set; }

        public List<ContractContentViewModel> ContractContents { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }
}