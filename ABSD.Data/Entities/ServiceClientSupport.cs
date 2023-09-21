using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ServiceClientSupport
    {
        [Required]
        public int ClientSupportId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("ClientSupportId")]
        public ClientSupport ClientSupport { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}