// Type: Microsoft.WindowsAzure.StorageClient.TableServiceContext
// Assembly: Microsoft.WindowsAzure.StorageClient, Version=1.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files\Windows Azure SDK\v1.6\ref\Microsoft.WindowsAzure.StorageClient.dll

using Microsoft.WindowsAzure;
using System;
using System.Data.Services.Client;

namespace Microsoft.WindowsAzure.StorageClient
{
    public class TableServiceContext : DataServiceContext
    {
        public TableServiceContext(string baseAddress, StorageCredentials credentials);
        public RetryPolicy RetryPolicy { get; set; }
        public StorageCredentials StorageCredentials { get; }
        public IAsyncResult BeginSaveChangesWithRetries(AsyncCallback callback, object state);
        public IAsyncResult BeginSaveChangesWithRetries(SaveChangesOptions options, AsyncCallback callback, object state);
        public DataServiceResponse EndSaveChangesWithRetries(IAsyncResult asyncResult);
        public DataServiceResponse SaveChangesWithRetries();
        public DataServiceResponse SaveChangesWithRetries(SaveChangesOptions options);
    }
}
