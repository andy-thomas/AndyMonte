using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace AndyMonte.Calculator
{
    // Andy
    // Tutorial
    // See http://social.technet.microsoft.com/wiki/contents/articles/2128.aspx

    public class ProjectCalculationEntry : TableServiceEntity
    {
        public ProjectCalculationEntry()
        {
            PartitionKey = DateTime.UtcNow.ToString("ddMMyyyy");
            // Row key allows sorting, so we make sure the rows come back in time order
            RowKey = string.Format("{0:10}_{1}", DateTime.MaxValue.Ticks - DateTime.Now.Ticks, Guid.NewGuid());
        }

        public string ProjectName { get; set; }
        public double Duration { get; set; }
        //public string ImageURL { get; set; }
        //public string ThumbnailURL { get; set; }
    }
}
