using ABSD.Data.Entities;
using System.Collections.Generic;

namespace ABSD.Application.ViewModels
{
    public class ContractViewModel
    {
        public int Id { get; set; }
        public List<ContractContentViewModel> ContractContents { get; set; }
        public string ContractName { get; set; }
        public ServiceViewModel Service { get; set; }
    }
}