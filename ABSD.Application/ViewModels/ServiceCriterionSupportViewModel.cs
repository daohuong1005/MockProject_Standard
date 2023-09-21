using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ServiceCriterionSupportViewModel
    {
        [Required]
        public int CriterionId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public CriterionViewModel Criterion { get; set; }

        public ServiceViewModel Service { get; set; }
    }
}