using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndyMonte.Calculator
{
    public static class Extensions
    {
        public static List<int> Bucketize(this IEnumerable<double> source, int totalBuckets)
        {
            // http://stackoverflow.com/questions/2387916/looking-for-a-histogram-binning-algorithm-for-decimal-data
            var min = double.MaxValue;
            var max = double.MinValue;
            var buckets = new List<int>();

            min = source.Min();
            max = source.Max();

            //foreach (var value in source)
            //{
            //    min = Math.Min(min, value);
            //    max = Math.Max(max, value);
            //}
            var bucketSize = (max - min) / totalBuckets;
            foreach (var value in source)
            {
                int bucketIndex = 0;
                if (bucketSize > 0.0)
                {
                    bucketIndex = (int)((value - min) / bucketSize);
                    if (bucketIndex == totalBuckets)
                    {
                        bucketIndex--;
                    }
                }
                buckets[bucketIndex]++;
            }
            return buckets;
        } 

    }
}
