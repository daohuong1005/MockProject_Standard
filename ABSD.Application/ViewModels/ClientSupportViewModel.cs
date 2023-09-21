using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ClientSupportViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }
        public string TypeName { get; set; }

        public List<ServiceClientSupportViewModel> ServiceClientSupport { get; set; }
    }
}