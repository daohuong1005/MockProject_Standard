using ABSD.Data.Entities;
using System.Collections.Generic;

namespace ABSD.Application.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string ContactType { get; set; }
        public bool IsActive { get; set; }
        public List<Funding> Fundings { get; set; }
    }
}