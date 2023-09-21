using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class PremiseViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string LocationName { get; set; }

        [MaxLength(500)]
        public string AddressLine { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public string ProjectCode { get; set; }
    }
}