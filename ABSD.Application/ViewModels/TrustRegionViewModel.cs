using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class TrustRegionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please input the Region Name")]
        [MaxLength(250, ErrorMessage = "RegionName has maximum 250 characters")]
        public string RegionName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Please select the Nation/Country")]
        public int CountryId { get; set; }

        public CountryViewModel Country { get; set; }
        public List<TrustDistrictViewModel> Districts { get; set; }
    }
}