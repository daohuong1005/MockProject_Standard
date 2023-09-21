using System.Collections.Generic;

namespace ABSD.Data.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string ContactType { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Funding> Fundings { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}