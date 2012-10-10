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
using Troschuetz.Random;

namespace SimulationRunWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        const string QueueName = "simulation";

        private const bool UseMessageBus = true;

        // QueueClient is thread-safe. Recommended that you cache 
        // rather than recreating it on every request
        QueueClient QueueClient;

        private CalculatorDataSource dataSource;
        bool IsStopped;
        
        public override void Run()
        {
            while (!IsStopped)
            {
                Project project = null;
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

                            project = receivedMessage.GetBody<Project>();

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
                    //------------------------------------
                    // Pick up a message from the "simulation" message queue.
                    // Create a TaskAggregator and call GenerateAggregatedDuration
                    // Save the simulation result (project name, run number, calculated duration)
                    // to Azure table storage
                    //using (CalculatorDataSource dataSource = new CalculatorDataSource("simulation"))
                    //{
                        project = dataSource.GetProjectFromQueueMessage();
                    //}

                    //------------------------------------
                }

                if (project != null)
                    {
                        // Create a random number generator
                        Distribution randomNumberGenerator = new FisherTippettDistribution();

                        // Create a TaskAggregator, passing in the RNG and the project
                        TaskAggregator taskAggregator = new TaskAggregator(project, randomNumberGenerator);
                        double aggregatedDuration = taskAggregator.GenerateAggregatedDuration();
                        dataSource.AddEntry(
                            new ProjectCalculationEntry { Duration = aggregatedDuration, ProjectName = project.Name },
                            project.Name);
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
               
            }

            dataSource = new CalculatorDataSource("simulation");
            IsStopped = false;
            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            if(dataSource!= null) dataSource.Dispose();
            IsStopped = true;
            QueueClient.Close();
            base.OnStop();
        }
    }
}
