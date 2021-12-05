using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EVTrend.Areas.Content.Models
{
    public class TotalElecModel
    {
        public string Year { get; set; }
        public string Country { get; set; }
        public float ElecRegisterNumber { get; set; }
        public float TotalRegisterNumber { get; set; }

    }
}