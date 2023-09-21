using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ServiceAccreditation
    {
        public int ServiceId { get; set; }
        public int AccreditationId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [ForeignKey("AccreditationId")]
        public virtual Accreditation Accreditation { get; set; }
    }
}