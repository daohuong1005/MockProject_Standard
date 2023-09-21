using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Content
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string ContentName { get; set; }

        [Required]
        public int Type { get; set; }

        public virtual ICollection<ContractContent> ContractContents { get; set; }
    }
}