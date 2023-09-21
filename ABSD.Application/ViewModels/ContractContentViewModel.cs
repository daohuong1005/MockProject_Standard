namespace ABSD.Application.ViewModels
{
    public class ContractContentViewModel
    {
        public int ParticipationId { get; set; }
        public int ContentId { get; set; }
        public int ContractId { get; set; }
        public ContentViewModel Content { get; set; }
        public ParticipationViewModel Participations { get; set; }
        public ContractViewModel Contract { get; set; }
    }
}