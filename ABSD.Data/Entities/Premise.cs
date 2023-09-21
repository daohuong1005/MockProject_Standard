using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Premise
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string LocationName { get; set; }

        [MaxLength(500)]
        public string AddressLine { get; set; }

        [MaxLength(20)]
        [DataType("varchar")]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<ServicePremises> ServicePremises { get; set; }
    }
}