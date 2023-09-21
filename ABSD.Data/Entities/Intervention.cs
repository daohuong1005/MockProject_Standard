using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Intervention
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string InterventionName { get; set; }

        public virtual ICollection<ServiceIntervention> ServiceInterventions { get; set; }
    }
}