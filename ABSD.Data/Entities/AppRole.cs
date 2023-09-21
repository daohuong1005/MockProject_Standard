using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ABSD.Data.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<RoleOrganization> RoleOrganizations { get; set; }
    }
}