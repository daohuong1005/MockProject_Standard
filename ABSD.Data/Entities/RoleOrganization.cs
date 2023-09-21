using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class RoleOrganization
    {
        public int OrganizationId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        [ForeignKey("RoleId")]
        public AppRole AppRole { get; set; }
    }
}