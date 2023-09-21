using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class ServiceType
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}