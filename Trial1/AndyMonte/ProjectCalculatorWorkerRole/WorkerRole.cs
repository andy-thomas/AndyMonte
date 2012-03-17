// ReSharper disable FunctionNeverReturns
using System.Diagnostics;
using System.Net;
using System.Threading;
using AndyMonte.Calculator;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ProjectCalculatorWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("$projectname$ entry point called", "Information");

            while (true)
            {
                //Thread.Sleep(1000);
                //Trace.WriteLine("Working", "Information");

                // Pick up messages from the "Main" message queue sent from the website
                // which provides info about the project (name, number of tasks, etc)

                // Then use the ProjectCalculator object to InitiateCalculation
                // This creates a Task Aggregator and call the InitiateSimulations method
                // which fires off one message for each simulation run 
                // to a "simulation" message queue.
                // This will get picked up by a different worker process.
                using (CalculatorDataSource dataSource = new CalculatorDataSource("main"))
                {
                    CalculationParameters calculationParameters =
                        dataSource.GetObjectFromQueueMessage<CalculationParameters>();
                    if (calculationParameters != null)
                    {
                        string projectName = calculationParameters.ProjectName;
                        int iterationCount = calculationParameters.IterationCount;

                        if (!string.IsNullOrEmpty(projectName))
                        {
                            // Now we have the project name, create a new Calculator and initiate the calculation
                            ProjectCalculator calculator = new ProjectCalculator(projectName, iterationCount);
                            calculator.InitiateCalculation();


                            // Now we have initiated a project, we can pause for a while
                            Thread.Sleep(100000);
                        }
                    }
                }

                //---------------------------------------
                ////APT
                //// Dequeue a message
                //// http://www.windowsazure.com/en-us/develop/net/how-to-guides/queue-service

                //// Retrieve storage account from connection-string
                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                //    RoleEnvironment.GetConfigurationSettingValue("AndyMonteStorageConnectionString"));

                //// Create the queue client
                //CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                //// Retrieve a reference to a queue
                //CloudQueue queue = queueClient.GetQueueReference("myqueue");

                //// Get the next message
                //CloudQueueMessage retrievedMessage = queue.GetMessage();
                //if (retrievedMessage != null)
                //{
                //    Console.WriteLine(retrievedMessage.AsString);

                //    //Process the message in less than 30 seconds, and then delete the message
                //    try
                //    {

                //        queue.DeleteMessage(retrievedMessage);
                //    }
                //    catch
                //    {
                //        // Do nothing - do not delete the message from the queue
                //    }
                //    //-------------------------------------------
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
