using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndyMonte.Calculator
{
    public class AggregationSummary
    {
        public List<DistributionPoint> DistributionPoints { get; set; }
        public double MeanDuration { get; set; }
        public double Variance { get; set; }
        public string ProjectName { get; set; }
        public int TaskCount { get; set; }

    }
}
