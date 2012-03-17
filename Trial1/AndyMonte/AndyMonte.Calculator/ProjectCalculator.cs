using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using Troschuetz.Random;

namespace AndyMonte.Calculator
{
    public class ProjectCalculator
    {
        private string projectName;

        public ProjectCalculator(string projectName)
        {
            this.projectName = projectName;
        }

        public AggregationSummary GenerateSummary()
        {
            Project project = GetProject(projectName);
            

            //-------------------------------------------------------------------------------------------------
            //// This stuff will get refactored out to when the calulation is submitted

            //// Create a random number generator
            //Distribution randomNumberGenerator = new FisherTippettDistribution();

            //// Create a TaskAggregator, passing in the RNG and the project
            //TaskAggregator taskAggregator = new TaskAggregator(project, randomNumberGenerator, 100000);

            //// Invoke the TaskAggregator Aggregate method, returning a List<double> of numbers
            //List<double> aggregatedDurations = taskAggregator.Aggregate();
            //-------------------------------------------------------------------------------------------------

            // TODO: Get the list of aggregated durations from Azure storage
            CalculatorDataSource dataSource = new CalculatorDataSource(projectName);
            IEnumerable<ProjectCalculationEntry> entries = dataSource.GetEntries(projectName);
  
            List<double> aggregatedDurations = new List<double>();
            foreach (ProjectCalculationEntry projectCalculationEntry in entries)
            {
                aggregatedDurations.Add(projectCalculationEntry.Duration);
            }

            // Calculate the mean and variance
            double mean = CalculateMean(aggregatedDurations);
            double variance = CalculateVariance(aggregatedDurations, mean);

            // Get the distribution of durations
            List<DistributionPoint> dataPoints = TransformToDistribuion(aggregatedDurations);

            // Create and return an AggregationSummary, passing in the List, mean, variance and task count and project name
            return new AggregationSummary() { 
                ProjectName = project.Name,
                DistributionPoints = dataPoints,
                MeanDuration = mean,
                Variance = variance,
                TaskCount = project.Tasks.Count
            };
        }

        private List<DistributionPoint> TransformToDistribuion(List<double> aggregatedDurations)
        {
            int abscissaCount = 50;
            List<DistributionPoint> distributionPoints = new List<DistributionPoint>(abscissaCount);
            for (int i = 0; i < abscissaCount; i++)
            {
                distributionPoints.Add(new DistributionPoint());
            }

            double minValue = aggregatedDurations.Min();
            double maxValue = aggregatedDurations.Max();

            int index = 0;
            foreach (DistributionPoint distributionPoint in distributionPoints)
            {
                distributionPoint.Duration = minValue + (((maxValue - minValue) / abscissaCount) * index++);
            }

            var bucketSize = (maxValue - minValue) / abscissaCount;
            foreach (var value in aggregatedDurations)
            {
                int bucketIndex = 0;
                if (bucketSize > 0.0)
                {
                    bucketIndex = (int)((value - minValue) / bucketSize);
                    if (bucketIndex == abscissaCount)
                    {
                        bucketIndex--;
                    }
                }
                distributionPoints[bucketIndex].Count++;
                distributionPoints[bucketIndex].Probability = distributionPoints[bucketIndex].Count/
                                                              aggregatedDurations.Count;

            }
            return distributionPoints;
        }

        private double CalculateVariance(List<double> aggregatedDurations, double mean)
        {
            List<double> deviations = new List<double>(aggregatedDurations.Capacity);
            deviations.AddRange(aggregatedDurations.Select(duration => Math.Pow((duration - mean), 2)));
            return deviations.Average();
        }

        private double CalculateMean(IEnumerable<double> aggregatedDurations)
        {
            return aggregatedDurations.Average();
        }

        
        private Project GetProject(string projectName)
        {
            // Get project from repository
            Project project = new Project
                                  {
                                      Name = projectName, 
                                      Tasks = SeedTasks()
                                  };

            return project;
        }

        private List<Task> SeedTasks()
        {
            const string namePrefix = "Test Task";
            List<Task> tasks = new List<Task>();
            tasks.Add(new Task(namePrefix, 10, 2));
            tasks.Add(new Task(namePrefix, 30, 5));
            tasks.Add(new Task(namePrefix, 5, 2));
            tasks.Add(new Task(namePrefix, 50, 6));

            return tasks;
        }

        //========================

        // This is used by the web site. 
        // It fires off messages to the Azure queue
        // which will get picked up by the service (which also uses this class!)
        public void InitiateCalculation()
        {
            Project project = GetProject(projectName);

            // Create a TaskAggregator, passing in the project
            int iterationCount = 100; //TODO: Centralise the variable
            CalculatorDataSource calculatorDataSource = new CalculatorDataSource("simulation");

            for (int i = 0; i < iterationCount; i++)
            {
                //  _project.Tasks

                // queue a message to process the image
                calculatorDataSource.EnQueue(project);

                //CloudQueueMessage message = new CloudQueueMessage(String.Format("{0},{1},{2}", uri, entry.PartitionKey, entry.RowKey));
                //queue.AddMessage(message);
            }
        }
    }
}
