using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string OrgName { get; set; }

        [MaxLength(500)]
        public string ShortDescription { get; set; }

        public string RoleViewModels { get; set; }
    }
}