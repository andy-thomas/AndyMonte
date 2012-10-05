using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AndyMonte.Calculator
{
    public class AggregationSummary
    {
        // Project structure
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Number of tasks")]
        public int TaskCount { get; set; }
        
        // Calculation Result summary
        public List<DistributionPoint> DistributionPoints { get; set; }
        [DisplayName("Mea Duration")]
        public double MeanDuration { get; set; }
        public double Variance { get; set; }
 
        public TimeSpan CalculationTime { get; set; }
        [DisplayName("Calculation time (seconds)")]
        public string CalculationTimeInSeconds
        {
            get 
            { 
                return ((int) CalculationTime.TotalSeconds).ToString();
            }
        }

    }
}
