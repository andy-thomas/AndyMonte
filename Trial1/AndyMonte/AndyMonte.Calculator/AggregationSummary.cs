using System;
using System.Collections.Generic;

namespace AndyMonte.Calculator
{
    public class AggregationSummary
    {
        // Project structure
        public string ProjectName { get; set; }
        public int TaskCount { get; set; }
        
        // Calculation Result summary
        public List<DistributionPoint> DistributionPoints { get; set; }
        public double MeanDuration { get; set; }
        public double Variance { get; set; }
        public TimeSpan CalculationTime { get; set; }

    }
}
