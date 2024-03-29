﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.MobileService.Dto
{
    public class ReviewDto
    {
        public DateTime Date { get; set; }
        public DateTime NextReviewDate { get; set; }
        public string Note { get; set; }
        public NominalDto HealthCenter { get; set; }
        public string Description { get; set; }
        public string Recipe { get; set; }
        public NominalDto Doctor { get; set; }
    }
}
