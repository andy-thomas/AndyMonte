// Type: Microsoft.WindowsAzure.StorageClient.CloudTableClient
// Assembly: Microsoft.WindowsAzure.StorageClient, Version=1.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files\Windows Azure SDK\v1.6\ref\Microsoft.WindowsAzure.StorageClient.dll

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient.Protocol;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;

namespace Microsoft.WindowsAzure.StorageClient
{
    public class CloudTableClient
    {
        public CloudTableClient(string baseAddress, StorageCredentials credentials);
        public CloudTableClient(Uri baseAddressUri, StorageCredentials credentials);
        public DateTime MinSupportedDateTime { get; }
        public Uri BaseUri { get; }
        public RetryPolicy RetryPolicy { get; set; }
        public TimeSpan Timeout { get; set; }
        public StorageCredentials Credentials { get; }
        public static void CreateTablesFromModel(Type serviceContextType, string baseAddress, StorageCredentials credentials);
        public TableServiceContext GetDataServiceContext();
        public void Attach(DataServiceContext serviceContext);
        public IAsyncResult BeginCreateTable(string tableName, AsyncCallback callback, object state);
        public void EndCreateTable(IAsyncResult asyncResult);
        public void CreateTable(string tableName);
        public IAsyncResult BeginCreateTableIfNotExist(string tableName, AsyncCallback callback, object state);
        public bool EndCreateTableIfNotExist(IAsyncResult asyncResult);
        public bool CreateTableIfNotExist(string tableName);
        public IAsyncResult BeginDoesTableExist(string tableName, AsyncCallback callback, object state);
        public bool EndDoesTableExist(IAsyncResult asyncResult);
        public bool DoesTableExist(string tableName);
        public IEnumerable<string> ListTables();
        public IEnumerable<string> ListTables(string prefix);
        public ResultSegment<string> ListTablesSegmented();
        public ResultSegment<string> ListTablesSegmented(int maxResults, ResultContinuation continuationToken);
        public ResultSegment<string> ListTablesSegmented(string prefix, int maxResults, ResultContinuation continuationToken);
        public IAsyncResult BeginListTablesSegmented(AsyncCallback callback, object state);
        public IAsyncResult BeginListTablesSegmented(string prefix, AsyncCallback callback, object state);
        public IAsyncResult BeginListTablesSegmented(string prefix, int maxResults, ResultContinuation continuationToken, AsyncCallback callback, object state);
        public ResultSegment<string> EndListTablesSegmented(IAsyncResult asyncResult);
        public IAsyncResult BeginDeleteTable(string tableName, AsyncCallback callback, object state);
        public void EndDeleteTable(IAsyncResult asyncResult);
        public void DeleteTable(string tableName);
        public IAsyncResult BeginDeleteTableIfExist(string tableName, AsyncCallback callback, object state);
        public bool EndDeleteTableIfExist(IAsyncResult asyncResult);
        public bool DeleteTableIfExist(string tableName);
        public ServiceProperties GetServiceProperties();
        public IAsyncResult BeginGetServiceProperties(AsyncCallback callback, object state);
        public ServiceProperties EndGetServiceProperties(IAsyncResult asyncResult);
        public void SetServiceProperties(ServiceProperties properties);
        public IAsyncResult BeginSetServiceProperties(ServiceProperties properties, AsyncCallback callback, object state);
        public void EndSetServiceProperties(IAsyncResult asyncResult);
        public event EventHandler<ResponseReceivedEventArgs> ResponseReceived;
    }
}
