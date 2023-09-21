using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class TrustDistrictViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "DistrictName is required")]
        [MaxLength(250, ErrorMessage = "DistrictName has maximum 250 characters")]
        public string DistrictName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }
        public TrustRegionViewModel Region { get; set; }
    }
}