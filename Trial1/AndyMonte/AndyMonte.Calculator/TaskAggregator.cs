using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.WindowsAzure.StorageClient;
using Troschuetz.Random;

namespace AndyMonte.Calculator
{
    public class TaskAggregator
    {
        private readonly Project _project;
        private readonly Distribution _randomNumberGenerator;
        private readonly int _iterationCount  ;

        public TaskAggregator(Project project, int iterationCount)
            : this (project, new FisherTippettDistribution(), iterationCount)
        {}

        public TaskAggregator(Project project, Distribution randomNumberGenerator, int iterationCount = 1000)
        {
            _project = project;
            _randomNumberGenerator = randomNumberGenerator;
            _randomNumberGenerator.Reset();
            _iterationCount = iterationCount;
        }

        internal List<double> Aggregate()
        {
            // make this ansynchronous
            // and then this can be delegated out to Azure worker roles
            // http://www.codeproject.com/Articles/15540/Monte-Carlo-Simulation-Using-Parallel-Asynchronous

            List<double> projectAggregatedDuration = new List<double>(_iterationCount);

            for (int i = 0; i < _iterationCount; i++)
            {

                // This is the bit which can be sent out to worker roles
                double aggregatedDuration = GenerateAggregatedDuration();
                projectAggregatedDuration.Add(aggregatedDuration);
            }
            return projectAggregatedDuration;
        }

        public double GenerateAggregatedDuration()
        {
            double aggregatedDuration = 0;
            foreach (Task task in _project.Tasks)
            {
                var simulationResult = SimulationCalculator.RunSimulation(task, _randomNumberGenerator);
                aggregatedDuration += simulationResult;
            }
            return aggregatedDuration;
        }

        // This is used by the web site. 
        // It fires off messages to the Azure queue
        internal void InitiateSimulations()
        {
            //for (int i = 0; i < _iterationCount; i++)
            //{
            //   //  _project.Tasks

            //   // Serialize the Project into a string
               
            //   // queue a message to process the image
            //    //CloudQueueMessage message = new CloudQueueMessage(String.Format("{0},{1},{2}", uri, entry.PartitionKey, entry.RowKey));
            //    //queue.AddMessage(message);
            //}
        }


    }
}
