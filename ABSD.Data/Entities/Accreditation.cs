using System.Collections.Generic;

namespace ABSD.Data.Entities
{
    public class Accreditation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ServiceAccreditation> ServiceAccreditations { get; set; }
    }
}