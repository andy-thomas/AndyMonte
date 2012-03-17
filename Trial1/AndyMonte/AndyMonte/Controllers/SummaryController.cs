using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AndyMonte.Calculator;

namespace AndyMonte.Controllers
{
    public class SummaryController : Controller
    {
        //
        // GET: /Summary/

        public ActionResult Index()
        {
            // Simply redirect to Submit, since Submit will serve as the
            // front page of this application
            return RedirectToAction("Submit");
        }

        public ActionResult Submit()
        {
            CalculationParameters parameters = new CalculationParameters {IterationCount = 100};
            return View(parameters);
        }

        // POST: /Home/Submit
        // Controler method for handling submissions from the submission
        // form 
        [HttpPost]
        public ActionResult Submit(CalculationParameters calculationParameters)
        {
            if (ModelState.IsValid)
            {
                // Will put code for submitting to queue here.
                string projectName = calculationParameters.ProjectName;
                int iterationCount = calculationParameters.IterationCount;

                InitiateCalculation(calculationParameters);
                //new ProjectCalculator(projectName).InitiateCalculation();

                return RedirectToAction("Waiting", new { id = projectName, iterationCount = iterationCount });
                //return RedirectToAction("Details", new { id = projectName });
                //return View("Details", summary);

            }
            else
            {
                return View(calculationParameters);
            }
        }

        private void InitiateCalculation(CalculationParameters calculationParameters)
        {
            CalculatorDataSource dataSource = new CalculatorDataSource("main");
            dataSource.EnQueue(calculationParameters);
        }

        //public ActionResult Index()
        //{
        //    string projectName = "Test Project";
        //    Calculator.AggregationSummary summary = new Calculator.ProjectCalculator(projectName).GenerateSummary();
        //    return View(summary);
        //}

        //
        // GET: /Summary/Details/5

        public ActionResult Details(string id, int iterationCount) //, AggregationSummary summary)
        {
            Console.WriteLine(id);
            string projectName = id;

            AggregationSummary summary = new ProjectCalculator(projectName, iterationCount).GenerateSummary();
            summary.CalculationTime = TimeSinceFirstRequest(projectName);
            AndyTimer.Reset();

            return View(summary);
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Waiting(string id, int iterationCount)
        {
            string projectName = id;
            TimeSpan timeSinceFirstRequest = TimeSinceFirstRequest(projectName);
            bool calculationIsReady = IsCalculationReady(projectName, iterationCount); //, timeSinceFirstRequest);
            if (calculationIsReady)
            {
                return RedirectToAction("Details", new {id = projectName, iterationCount = iterationCount});
            }
            else
            {
                // Refresh the entire page every six seconds
                Response.AddHeader("Refresh", "6");
                //return View(timeSinceFirstRequest);

                // display a timer on the screen with a partial view which refreshes every second
                return View(timeSinceFirstRequest);
            }
        }

        private static bool IsCalculationReady(string projectName, int iterationCount) //, TimeSpan timeSinceFirstRequest)
        {
            // Go to the storage and find if all the simulation calculations have been returned

            //bool calculationIsReady = timeSinceFirstRequest.Seconds > 10; // HACK
            bool calculationIsReady = false;

            try
            {
                CalculatorDataSource dataSource = new CalculatorDataSource("simulation");
                IEnumerable<ProjectCalculationEntry> entries = dataSource.GetEntries(projectName);
                int count = entries.Count();
                calculationIsReady = count == iterationCount;

            }
            catch 
            {
                // do nothing
            } 
            return calculationIsReady;
        }

        private static TimeSpan TimeSinceFirstRequest(string projectName)
        {
            // TODO Make the elapsed time project-specific
            // Get the time since the calculations started for this project
            // For now, let's just use a dummy "AndyTimer" class to deal with all/any projects
            DateTime startTime = AndyTimer.GetTime();
            TimeSpan timeSinceFirstRequest = DateTime.Now - startTime;
            return timeSinceFirstRequest;
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public PartialViewResult ElapsedTime()
        {
            DateTime startTime = AndyTimer.GetTime();
            TimeSpan timeSinceFirstRequest = DateTime.Now - startTime;
            return PartialView("_ElapsedTime", timeSinceFirstRequest);
        }

        #region Unused Code
        ////
        //// GET: /Summary/Create

        //public ActionResult Create()
        //{
        //    return View();
        //} 

        ////
        //// POST: /Summary/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Summary/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Summary/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Summary/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Summary/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //private void EnqueueSummaryMessage(string messageText)
        //{
        //    //http://www.windowsazure.com/en-us/develop/net/how-to-guides/queue-service/

        //    // Retrieve storage account from connection-string
        //    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
        //        RoleEnvironment.GetConfigurationSettingValue("AndyMonteStorageConnectionString"));

        //    // Create the queue client
        //    CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

        //    // Retrieve a reference to a queue
        //    CloudQueue queue = queueClient.GetQueueReference("myqueue");

        //    // Create the queue if it doesn't already exist
        //    queue.CreateIfNotExist();

        //    // Create a message and add it to the queue
        //    CloudQueueMessage message = new CloudQueueMessage(messageText);
        //    queue.AddMessage(message);
        //} 
        #endregion

    }

    public static class AndyTimer
    {
        private static DateTime? _dateTime;
        public static DateTime GetTime()
        {
            if (!_dateTime.HasValue)
                _dateTime = DateTime.Now;
            return _dateTime.Value;
        }

        internal static void Reset()
        {
            _dateTime = null;
        }
    }
}
