using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ServicePremiseViewModel
    {
        public int ServiceId { get; set; }
        public int PremiseId { get; set; }

        [MaxLength(20)]
        public string ProjectCode { get; set; }

        //public Service Service { get; set; }
        //public Premise Premise { get; set; }
    }
}