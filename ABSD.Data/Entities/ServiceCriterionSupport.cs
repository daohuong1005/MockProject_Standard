using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ServiceCriterionSupport
    {
        [Required]
        public int CriterionId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("CriterionId")]
        public virtual Criterion Criterion { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}