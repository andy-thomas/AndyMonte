// ReSharper disable InconsistentNaming
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace AndyMonte.Calculator
{
    public class CalculatorDataSource : IDisposable
    {

        private const string messageTableName = "MessageTable";
        private const string connectionStringName = "AndyMonteStorageConnectionString"; //"DataConnectionString";
        private static CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;

        private const string messageImageBlobName = "andymonteblobs"; //Note: All letters in a container name must be lowercase. For more restrictions on container names, see http://msdn.microsoft.com/en-us/library/dd135715.aspx
        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        private string messageQueueName; //= "andymontequeue"; //queue name must be in lower case
        private CloudQueueClient queueClient;
        private CloudQueue queue;

        public CalculatorDataSource() : this("andymontequeue")
        {
        }

        public CalculatorDataSource(string queueName)
        {
            string connectionString;
           messageQueueName = queueName;
            try
            {
                connectionString = RoleEnvironment.GetConfigurationSettingValue(connectionStringName);
            }
            catch
            {
                // Crap - why does the one of the roles not have the connection string correctly configured????
                connectionString = @"UseDevelopmentStorage=true";
            }

            storageAccount = CloudStorageAccount.Parse(connectionString);
            tableClient = new CloudTableClient(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials);
            tableClient.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));
            tableClient.CreateTableIfNotExist(messageTableName);
            
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(messageImageBlobName);
            blobContainer.CreateIfNotExist();

            var permissions = blobContainer.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            blobContainer.SetPermissions(permissions);

            //create queue
            queueClient = storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference(messageQueueName);
            queue.CreateIfNotExist();
        }

        public IEnumerable<ProjectCalculationEntry> GetEntries(string tableName)
        {
            TableServiceContext tableServiceContext = tableClient.GetDataServiceContext();
            var query = from g in tableServiceContext.CreateQuery<ProjectCalculationEntry>(tableName)
                          //where g.ProjectName == projectName                          
                          select g;

            // Read rows if there are more than 1000 - see http://blogs.msdn.com/b/rihamselim/archive/2011/01/06/retrieving-more-the-1000-row-from-windows-azure-storage.aspx
            var allItemsAsTableService = query.AsTableServiceQuery();
            IEnumerable<ProjectCalculationEntry> allItems = allItemsAsTableService.Execute();

            return allItems;
        }

        public void AddEntry(ProjectCalculationEntry newItem, string tableName)
        {
            try
            {
                tableClient.CreateTableIfNotExist(tableName);

                TableServiceContext tableServiceContext = tableClient.GetDataServiceContext();
                tableServiceContext.AddObject(tableName, newItem);
                tableServiceContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public string AddBlob(string fileExtension, string fileContentType, Stream fileContent)
        {
            // upload the image to blob storage

            string uniqueBlobName = string.Format(messageImageBlobName + "/image_{0}{1}", Guid.NewGuid().ToString(), fileExtension);
            CloudBlockBlob blob = blobClient.GetBlockBlobReference(uniqueBlobName);
            blob.Properties.ContentType = fileContentType;
            blob.UploadFromStream(fileContent);
            return blob.Uri.ToString();
        }

        //add message to the queue
        public void EnQueue(string uri, ProjectCalculationEntry entry)
        {
            // queue a message to process the image
            CloudQueueMessage message = new CloudQueueMessage(String.Format("{0},{1},{2}", uri, entry.PartitionKey, entry.RowKey));
            queue.AddMessage(message);
        }

        //add message to the queue
        public void EnQueue(string messageText)
        {
            // queue a message to process the image
            CloudQueueMessage message = new CloudQueueMessage(messageText);
            queue.AddMessage(message);
        }

        //add message to the queue
        public void EnQueue(object messageObject)
        {
            // queue a message to process the image
            string messageText = SerializeToString(messageObject);
            CloudQueueMessage message = new CloudQueueMessage(messageText);
            queue.AddMessage(message);
        }

        public Project GetProjectFromQueueMessage()
        {
            // retrieve a new message from the queue
            CloudQueueMessage msg = queue.GetMessage();

            if (msg != null)
            {
                // parse message retrieved from queue
                Project project = SerializeFromString<Project>(msg.AsString);

                queue.DeleteMessage(msg);
                return project;
            }
            return null;
        }

        public T GetObjectFromQueueMessage<T>() 
            where T : class 
        {
            // retrieve a new message from the queue
            CloudQueueMessage msg = queue.GetMessage();

            if (msg != null)
            {
                // parse message retrieved from queue
                T returnObject = SerializeFromString<T>(msg.AsString);

                queue.DeleteMessage(msg);
                return returnObject;
            }
            return null;
        }

        public string DequeueMessage()
        {
            string message = null;
            CloudQueueMessage msg = queue.GetMessage();

            if (msg != null)
            {
                message = msg.AsString;
                queue.DeleteMessage(msg);
            }
            return message;
        }

        //public void ProcessQueueMessage()
        //{
        //    // retrieve a new message from the queue
        //    CloudQueueMessage msg = queue.GetMessage();

        //    if (msg != null)
        //    {
        //        // parse message retrieved from queue
        //        var messageParts = msg.AsString.Split(new char[] { ',' });
        //        var imageBlobUri = messageParts[0];
        //        var partitionKey = messageParts[1];
        //        var rowkey = messageParts[2];

        //        //string thumbnailBlobUri = System.Text.RegularExpressions.Regex.Replace(imageBlobUri, "([^\\.]+)(\\.[^\\.]+)?$", "$1-thumb$2");
        //        //CloudBlob inputBlob = blobContainer.GetBlobReference(imageBlobUri);
        //        //CloudBlob outputBlob = blobContainer.GetBlobReference(thumbnailBlobUri);
        //        //using (BlobStream input = inputBlob.OpenRead())
        //        //using (BlobStream output = outputBlob.OpenWrite())
        //        //{
        //        //    CreateThumbnail(input, output);

        //        //    // commit the blob and set its properties
        //        //    output.Commit();
        //        //    outputBlob.Properties.ContentType = "image/jpeg";
        //        //    outputBlob.SetProperties();

        //        //    // update the entry in the table to point to the thumbnail
        //        //    UpdateThumbnailURL(partitionKey, rowkey, thumbnailBlobUri);

        //        //    queue.DeleteMessage(msg);
        //        //}
        //    }
        //}

        public static string SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }

        public static T SerializeFromString<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }


        ////create thumbnail for an image
        //private void CreateThumbnail(Stream input, Stream output)
        //{
        //    int width;
        //    int height;
        //    var originalImage = new Bitmap(input);
        //    if (originalImage.Width > originalImage.Height)
        //    {
        //        width = 128;
        //        height = 128 * originalImage.Height / originalImage.Width;
        //    }
        //    else
        //    {
        //        height = 128;
        //        width = 128 * originalImage.Width / originalImage.Height;
        //    }
        //    var thumbnailImage = new Bitmap(width, height);

        //    using (Graphics graphics = Graphics.FromImage(thumbnailImage))
        //    {
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //        graphics.DrawImage(originalImage, 0, 0, width, height);
        //    }

        //    thumbnailImage.Save(output, ImageFormat.Jpeg);
        //}

        ////update the thumbnail URL property for an entry.
        //public void UpdateThumbnailURL(string partitionKey, string rowKey, string thumbUrl)
        //{
        //    TableServiceContext tableServiceContext = tableClient.GetDataServiceContext();

        //    var results = from g in tableServiceContext.CreateQuery<ProjectCalculationEntry>(messageTableName)
        //                  where g.PartitionKey == partitionKey && g.RowKey == rowKey
        //                  select g;

        //    var entry = results.FirstOrDefault<ProjectCalculationEntry>();
        //    entry.ThumbnailURL = thumbUrl;
        //    tableServiceContext.UpdateObject(entry);
        //    tableServiceContext.SaveChanges();
        //}
        public void Dispose()
        {
            storageAccount = null;
            tableClient = null;
            blobClient = null;
            blobContainer = null;
            queueClient = null;
            queue = null;
        }

    }
}


// ReSharper restore InconsistentNaming