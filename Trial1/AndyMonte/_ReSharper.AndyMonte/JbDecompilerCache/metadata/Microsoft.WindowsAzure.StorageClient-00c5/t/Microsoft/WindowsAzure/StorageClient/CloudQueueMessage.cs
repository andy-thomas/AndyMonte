// Type: Microsoft.WindowsAzure.StorageClient.CloudQueueMessage
// Assembly: Microsoft.WindowsAzure.StorageClient, Version=1.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files\Windows Azure SDK\v1.6\ref\Microsoft.WindowsAzure.StorageClient.dll

using System;

namespace Microsoft.WindowsAzure.StorageClient
{
    public class CloudQueueMessage
    {
        public static readonly long MaxMessageSize;
        public static readonly TimeSpan MaxTimeToLive;
        public static readonly int MaxNumberOfMessagesToPeek;
        public CloudQueueMessage(byte[] content);
        public CloudQueueMessage(string content);
        public byte[] AsBytes { get; }
        public string Id { get; protected internal set; }
        public string PopReceipt { get; protected internal set; }
        public DateTime? InsertionTime { get; protected internal set; }
        public DateTime? ExpirationTime { get; protected internal set; }
        public DateTime? NextVisibleTime { get; protected internal set; }
        public string AsString { get; }
        public int DequeueCount { get; protected internal set; }
        protected internal string RawString { get; set; }
        public void SetMessageContent(string content);
        public void SetMessageContent(byte[] content);
    }
}
