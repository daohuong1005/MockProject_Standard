using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Criterion
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        public virtual ICollection<ServiceCriterionSupport> ServiceCriterionSupports { get; set; }
    }
}