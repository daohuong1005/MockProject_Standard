using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string ServiceName { get; set; }

        public string ServiceShortDescription { get; set; }
        public int? ContactId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? ProgrammeId { get; set; }
        public int? ParticipationId { get; set; }
        public int? ContractId { get; set; }

        public int? FundingId { get; set; }

        [MaxLength(250)]
        public string ClientDescription { get; set; }

        public DateTime? ServiceStartExpected { get; set; }
        public DateTime? ServiceStartDate { get; set; }
        public DateTime? ServiceEndDate { get; set; }
        public DateTime? ServiceExtendable { get; set; }
        public DateTime? ServiceTimeLimited { get; set; }
        public string ServiceFullDescription { get; set; }
        public bool ServiceActive { get; set; }

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

        public ICollection<ServiceAccreditation> ServiceAccreditations { get; set; }
        public ICollection<ServiceClientSupport> ServiceClientSupports { get; set; }
        public ICollection<ServiceCriterionSupport> ServiceCriterionSupports { get; set; }
        public ICollection<ServiceIntervention> ServiceInterventions { get; set; }
        public virtual Contract Contract { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }

        public virtual Funding Funding { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }

        [ForeignKey("ParticipationId")]
        public virtual Participation Participation { get; set; }

        public virtual ICollection<ServicePremises> ServicePremises { get; set; }
        public virtual ICollection<ServiceOrganization> ServiceOrganizations { get; set; }
    }
}