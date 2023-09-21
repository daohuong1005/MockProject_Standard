﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ABSD.Application.ViewModels
{
    public class CriterionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }
        public string TypeName { get; set; }

        public List<ServiceCriterionSupportViewModel> ServiceCriterionSupports { get; set; }
    }
}