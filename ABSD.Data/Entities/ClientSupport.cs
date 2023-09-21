using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class ClientSupport
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        public virtual ICollection<ServiceClientSupport> ServiceClientSupports { get; set; }
    }
}