using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class ContractContent
    {
        public int ParticipationId { get; set; }
        public int ContentId { get; set; }
        public int ContractId { get; set; }

        [ForeignKey("ContentId")]
        public virtual Content Content { get; set; }

        [ForeignKey("ParticipationId")]
        public virtual Participation Participation { get; set; }

        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
    }
}