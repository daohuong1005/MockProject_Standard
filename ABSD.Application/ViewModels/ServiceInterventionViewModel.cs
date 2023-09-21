using System;
using System.Collections.Generic;
using System.Text;

namespace ABSD.Application.ViewModels
{
    public class ServiceInterventionViewModel
    {
        public int ServiceId { get; set; }
        public int InterventionId { get; set; }

        public ServiceViewModel Service { get; set; }

        public InterventionViewModel Intervention { get; set; }
    }
}
