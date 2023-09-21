using System.Collections.Generic;

namespace ABSD.Data.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public virtual ICollection<ContractContent> ContractContents { get; set; }
        public string ContractName { get; set; }
        public virtual Service Service { get; set; }
    }
}