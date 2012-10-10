using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using AndyMonte.Calculator;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace ProjectCalculatorWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "main";

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        private CalculatorDataSource dataSource;
        QueueClient QueueClient;
        bool IsStopped;

        private const bool UseMessageBus = true;

        public override void Run()
        {
            while (!IsStopped)
            {
                CalculationParameters calculationParameters = null;
                if (UseMessageBus)
                {
                    try
                    {
                        // Receive the message
                        BrokeredMessage receivedMessage = null;
                        receivedMessage = QueueClient.Receive();

                        if (receivedMessage != null)
                        {
                            // Process the message
                            Trace.WriteLine("Processing", receivedMessage.SequenceNumber.ToString());

                            calculationParameters = receivedMessage.GetBody<CalculationParameters>();

                            receivedMessage.Complete();
                        }
                    }
                    catch (MessagingException e)
                    {
                        if (!e.IsTransient)
                        {
                            Trace.WriteLine(e.Message);
                            throw;
                        }

                        Thread.Sleep(10000);
                    }
                    catch (OperationCanceledException e)
                    {
                        if (!IsStopped)
                        {
                            Trace.WriteLine(e.Message);
                            throw;
                        }
                    }
                }
                else
                {
                    //---------------------------------
                    // Pick up messages from the "Main" message queue sent from the website
                    // which provides info about the project (name, number of tasks, etc)

                    // Then use the ProjectCalculator object to InitiateCalculation
                    // This creates a Task Aggregator and call the InitiateSimulations method
                    // which fires off one message for each simulation run 
                    // to a "simulation" message queue.
                    // This will get picked up by a different worker process.
                    //using (CalculatorDataSource dataSource = new CalculatorDataSource("main"))
                    //{
                    calculationParameters =
                        dataSource.GetObjectFromQueueMessage<CalculationParameters>();

                    //}
                    //---------------------------------
                }

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
                        Thread.Sleep(10000);
                    }
                }

            }
        }

        public override bool OnStart()
        {
            if (UseMessageBus)
            {
                // Set the maximum number of concurrent connections 
                ServicePointManager.DefaultConnectionLimit = 12;

                // Create the queue if it does not exist already
                string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
                var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
                if (!namespaceManager.QueueExists(QueueName))
                {
                    namespaceManager.CreateQueue(QueueName);
                }

                // Initialize the connection to Service Bus Queue
                QueueClient = QueueClient.CreateFromConnectionString(connectionString, QueueName);
            }
            else
            {
                dataSource = new CalculatorDataSource("main");
            }

            IsStopped = false;
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            dataSource.Dispose();
            IsStopped = true;
            QueueClient.Close();
            base.OnStop();
        }
    }
}
