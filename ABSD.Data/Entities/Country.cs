using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string CountryName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<TrustRegion> Regions { get; set; }
    }
}