using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class TrustDistrict
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string DistrictName { get; set; }

        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int TrustRegionId { get; set; }

        [ForeignKey("TrustRegionId")]
        public virtual TrustRegion Region { get; set; }
    }
}