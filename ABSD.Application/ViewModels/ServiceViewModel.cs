using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceShortDescription { get; set; }

        [Required]
        public int? FundingId { get; set; }
        public int? ContractId { get;  set; }
        public int? ContactId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? ParticipationId { get; set; }
        public string ClientDescription { get; set; }
        public DateTime? ServiceStartExpected { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public DateTime? ServiceExtendable { get; set; }
        public bool ServiceActive { get; set; }
        public string ServiceFullDescription { get; set; }

        [MaxLength(20)]
        [DataType("varchar")]
        public string DeptCode { get; set; }

        [MaxLength(250)]
        public string ServiceDesscriptionDelivery { get; set; }

        [MaxLength(20)]
        public string ServiceContactCode { get; set; }

        public int ServiceContractValue { get; set; }
        public bool ContractStagedPayment { get; set; }

        [MaxLength(50)]
        public string ReferralProcess { get; set; }

        public DateTime? ServiceTimeLimited { get; set; }
        public ServiceTypeViewModel ServiceTypeViewModel { get; set; }
        public ContactViewModel ContactViewModel { get; set; }
        public FundingViewModel FundingViewModel { get; set; }
        public ParticipationViewModel ParticipationViewModel { get; set; }
        public List<CriterionViewModel> CriterionViewModel { get; set; }
        public List<ClientSupportViewModel> ClientSupportViewModels { get; set; }
        public List<ContentViewModel> ContentViewModels { get; set; }
    }
}