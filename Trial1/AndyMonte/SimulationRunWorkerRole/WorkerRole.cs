// ReSharper disable FunctionNeverReturns
using System.Diagnostics;
using System.Net;
using AndyMonte.Calculator;
using Microsoft.WindowsAzure.ServiceRuntime;
using Troschuetz.Random;

namespace SimulationRunWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("$projectname$ entry point called", "Information");

            while (true)
            {
                //Thread.Sleep(200);
                // Make the simulation process a long running calculation ~200ms
                // (If it is too quick, you do not see the difference when adding new worker processes)
                //Trace.WriteLine("Working", "Information");

                // Pick up a message from the "simulation" message queue.
                // Create a TaskAggregator and call GenerateAggregatedDuration
                // Save the simulation result (project name, run number, calculated duration)
                // to Azure table storage
                using (CalculatorDataSource dataSource = new CalculatorDataSource("simulation"))
                {
                    Project project = dataSource.GetProjectFromQueueMessage();

                    if (project != null)
                    {
                        // Create a random number generator
                        Distribution randomNumberGenerator = new FisherTippettDistribution();

                        // Create a TaskAggregator, passing in the RNG and the project
                        TaskAggregator taskAggregator = new TaskAggregator(project, randomNumberGenerator);
                        double aggregatedDuration = taskAggregator.GenerateAggregatedDuration();
                        dataSource.AddEntry(
                            new ProjectCalculationEntry {Duration = aggregatedDuration, ProjectName = project.Name},
                            project.Name);
                    }
                }

                // Don't bother sending back a "done" message
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}

// ReSharper restore FunctionNeverReturns
