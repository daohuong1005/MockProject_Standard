using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ServicePremises
    {
        public int ServiceId { get; set; }
        public int PremiseId { get; set; }

        [MaxLength(20)]
        [DataType("varchar")]
        public string ProjectCode { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [ForeignKey("PremiseId")]
        public virtual Premise Premise { get; set; }
    }
}