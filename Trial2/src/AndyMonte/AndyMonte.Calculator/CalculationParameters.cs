// ReSharper disable EmptyConstructor
namespace AndyMonte.Calculator
{
    public class CalculationParameters
    {
        public CalculationParameters(){}   // Parameterless constructor need for XML serialization

        // Calculation details
        public int IterationCount { get; set; }
        
        // TODO add validation attribute (use Regex)
        public string ProjectName { get; set; }        
    }
}

// ReSharper restore EmptyConstructor
