using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Content.Models
{
    public class TotalCarbonModel
    {
        public string Year { get; set; }
        public string Country { get; set; }
        public float T_CarbonNumber { get; set; }
        public float TotalCarbonNumber { get; set; }
        public float Percentage { get; set; }

    }
}