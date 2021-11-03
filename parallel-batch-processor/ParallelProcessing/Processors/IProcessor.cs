using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelBatchProcessor.Processors
{
    public interface IProcessor<T>
    {
        bool Process(T id);
    }
}
