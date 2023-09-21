using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABSD.Data.Entities
{
    public class Funding
    {
        public int Id { get; set; }
        public int FundingSource { get; set; }

        [Required]
        public int ContactId { get; set; }

        [Required]
        public decimal FundingAmount { get; set; }

        public DateTime FundingStart { get; set; }
        public DateTime FundingEnd { get; set; }

        [MaxLength(250)]
        public string FundraisingForText { get; set; }

        [MaxLength(250)]
        public string FundraisingWhy { get; set; }

        public bool FundraisingDonorAnonymous { get; set; }
        public decimal FundraisingDonorAmount { get; set; }

        [MaxLength(50)]
        public string FundingNeeds { get; set; }

        public bool FundingContinuationNeeded { get; set; }

        [DataType("decimal(18,2)")]
        public decimal FundingContinuationAmount { get; set; }

        [MaxLength(250)]
        public string FundingContinuationDetails { get; set; }

        [MaxLength(50)]
        public string FundraisingNeeded { get; set; }

        public DateTime FundraisingRequiredBy { get; set; }
        public bool FundraisingComplete { get; set; }
        public DateTime FundraisingCompleteDate { get; set; }
        public DateTime FundraisingDonationDate { get; set; }
        public bool FundraisingDonationIncremental { get; set; }

        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}