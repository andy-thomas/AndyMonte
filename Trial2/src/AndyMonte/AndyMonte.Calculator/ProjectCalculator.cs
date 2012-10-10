using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace AndyMonte.Calculator
{
    public class ProjectCalculator
    {
        private readonly string _projectName;
        private readonly int _iterationCount;
        private const bool UseMessageBus = true;

        public ProjectCalculator(string projectName, int iterationCount)
        {
            _projectName = projectName;
            _iterationCount = iterationCount;
        }

        public AggregationSummary GenerateSummary()
        {
            Project project = GetProject(_projectName);
            

            //-------------------------------------------------------------------------------------------------
            //// This stuff will get refactored out to when the calulation is submitted

            //// Create a random number generator
            //Distribution randomNumberGenerator = new FisherTippettDistribution();

            //// Create a TaskAggregator, passing in the RNG and the project
            //TaskAggregator taskAggregator = new TaskAggregator(project, randomNumberGenerator, 100000);

            //// Invoke the TaskAggregator Aggregate method, returning a List<double> of numbers
            //List<double> aggregatedDurations = taskAggregator.Aggregate();
            //-------------------------------------------------------------------------------------------------

            // Get the list of aggregated durations from Azure storage
            CalculatorDataSource dataSource = new CalculatorDataSource();
            IEnumerable<ProjectCalculationEntry> entries = dataSource.GetEntries(_projectName);

            //foreach (ProjectCalculationEntry projectCalculationEntry in entries)
            //{
            //    aggregatedDurations.Add(projectCalculationEntry.Duration);
            //}
            List<double> aggregatedDurations = entries.Select(projectCalculationEntry => projectCalculationEntry.Duration).ToList();

            // Calculate the mean and variance
            double mean = CalculateMean(aggregatedDurations);
            double variance = CalculateVariance(aggregatedDurations, mean);

            // Get the distribution of durations
            List<DistributionPoint> dataPoints = TransformToDistribuion(aggregatedDurations);

            // Create and return an AggregationSummary, passing in the List, mean, variance and task count and project name
            return new AggregationSummary
                       { 
                ProjectName = project.Name,
                DistributionPoints = dataPoints,
                MeanDuration = mean,
                Variance = variance,
                TaskCount = project.Tasks.Count
            };
        }

        private List<DistributionPoint> TransformToDistribuion(List<double> aggregatedDurations)
        {
            const int abscissaCount = 50;
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
            List<Task> tasks = new List<Task>
                                   {
                                       new Task(namePrefix, 10, 2),
                                       new Task(namePrefix, 30, 5),
                                       new Task(namePrefix, 5, 2),
                                       new Task(namePrefix, 50, 6)
                                   };

            return tasks;
        }

        //========================

        // This is used by the ProjectCalculator Worker Role 
        // It fires off messages to the Azure queue
        // which will get picked up by the service (which also uses this class!)
        public void InitiateCalculation()
        {
            Project project = GetProject(_projectName);

            if (UseMessageBus)
            {
                // Create the queue if it does not exist already
                const string QueueName = "simulation";
                string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
                var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
                if (!namespaceManager.QueueExists(QueueName))
                {
                    namespaceManager.CreateQueue(QueueName);
                }

                // Initialize the connection to Service Bus Queue
                QueueClient queueClient = QueueClient.CreateFromConnectionString(connectionString, QueueName);
                
                // For each simulation run, queue a message to simulate the project estimation                
                for (int i = 0; i < _iterationCount; i++)
                {
                    BrokeredMessage message = new BrokeredMessage(project);
                    queueClient.Send(message);
                }
            }
            else
            {
                // Create a TaskAggregator, passing in the project
                CalculatorDataSource calculatorDataSource = new CalculatorDataSource("simulation");

                // For each simulation run, queue a message to simulate the project estimation
                for (int i = 0; i < _iterationCount; i++)
                {
                    calculatorDataSource.EnQueue(project);
                }
            }
        }
    }

}
