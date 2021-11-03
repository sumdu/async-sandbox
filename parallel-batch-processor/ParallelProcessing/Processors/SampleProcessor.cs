using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelBatchProcessor.Processors
{
    public class SampleProcessor : IProcessor<int>
    {
        public static object Lock = new object();

        public bool Process(int id)
        {
            //lock (Lock)
            //{
            //    Console.WriteLine($"Processing item {id}");
            //}

            Thread.Sleep(1000);
            return true;
        }
    }
}
