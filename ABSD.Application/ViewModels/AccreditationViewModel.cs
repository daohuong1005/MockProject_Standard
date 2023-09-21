using System.Collections.Generic;

namespace ABSD.Application.ViewModels
{
    public class AccreditationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ServiceAccreditationViewModel> serviceAccreditations { get; set; }
    }
}