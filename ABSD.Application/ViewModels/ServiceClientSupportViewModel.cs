using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ServiceClientSupportViewModel
    {
        [Required]
        public int ClientSupportId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public ClientSupportViewModel ClientSupport { get; set; }

        public ServiceViewModel Service { get; set; }
    }
}