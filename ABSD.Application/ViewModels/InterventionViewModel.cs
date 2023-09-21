using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class InterventionViewModel
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string InterventionName { get; set; }

        public List<ServiceInterventionViewModel> ServiceInterventions { get; set; }
    }
}