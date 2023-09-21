using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class TrustRegion
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string RegionName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual ICollection<TrustDistrict> Districts { get; set; }
    }
}