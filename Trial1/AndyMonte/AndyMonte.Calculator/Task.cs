using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndyMonte.Calculator
{
    public class Task
    {
        public Task(string name, double length, double uncertainty):this()
        {
            Name = name;
            Length = length;
            Uncertainty = uncertainty;
        }

        public Task()
        {
            // Required for XML Serialization
        }

        public string Name { get; set; }
        public double Length { get; set; }
        public double Uncertainty { get; set; }
    }
}
