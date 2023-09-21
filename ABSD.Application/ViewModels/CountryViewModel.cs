using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CountryName is required")]
        [MaxLength(250, ErrorMessage = "CountryName has maximum 250 characters")]
        public string CountryName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<TrustRegionViewModel> Regions { get; set; }
    }
}