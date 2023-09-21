using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ServiceOrganization
    {
        public int ServiceId { get; set; }
        public int OrganizationId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }
    }
}