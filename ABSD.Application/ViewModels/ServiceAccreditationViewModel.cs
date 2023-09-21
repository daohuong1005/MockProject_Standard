namespace ABSD.Application.ViewModels
{
    public class ServiceAccreditationViewModel
    {
        public int ServiceId { get; set; }
        public int AccreditationId { get; set; }

        public ServiceViewModel Service { get; set; }

        public AccreditationViewModel Accreditation { get; set; }
    }
}