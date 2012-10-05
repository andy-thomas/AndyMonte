using System.Diagnostics;
using Troschuetz.Random;

namespace AndyMonte.Calculator
{
    public class SimulationCalculator
    {
        public static double RunSimulation(Task task, Distribution randomNumberGeneratorParameter)
        {
            FisherTippettDistribution randomNumberGenerator = randomNumberGeneratorParameter as FisherTippettDistribution;
            Debug.Assert(randomNumberGenerator != null, "randomNumberGenerator must be not null");

            randomNumberGenerator.Mu = task.Length;
            randomNumberGenerator.Alpha = task.Uncertainty;
            double simulationResult = randomNumberGenerator.NextDouble();
            return simulationResult;
        }
    }
}
