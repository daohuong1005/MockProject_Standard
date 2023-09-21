using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string OrgName { get; set; }

        [MaxLength(500)]
        public string ShortDescription { get; set; }

        public virtual ICollection<RoleOrganization> RoleOrganizations { get; set; }
        public virtual ICollection<ServiceOrganization> ServiceOrganizations { get; set; }
    }
}