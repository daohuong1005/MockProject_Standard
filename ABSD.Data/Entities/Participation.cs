using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Data.Entities
{
    public class Participation
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string ParticipationName { get; set; }

        public virtual ICollection<ContractContent> ContractContents { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}