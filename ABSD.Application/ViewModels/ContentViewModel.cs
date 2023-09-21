using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ContentViewModel
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string ContentName { get; set; }

        [Required]
        public int Type { get; set; }
        public string TypeName { get; set; }

        public List<ContractContentViewModel> ContractContents { get; set; }
    }
}