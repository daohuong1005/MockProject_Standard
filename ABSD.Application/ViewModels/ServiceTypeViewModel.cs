using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ServiceTypeViewModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public List<ServiceViewModel> ServiceViewModels { get; set; }
    }
}