using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ServiceIntervention
    {
        public int ServiceId { get; set; }
        public int InterventionId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [ForeignKey("InterventionId")]
        public Intervention Intervention { get; set; }
    }
}