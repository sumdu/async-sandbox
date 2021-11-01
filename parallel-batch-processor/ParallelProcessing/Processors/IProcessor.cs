using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProcessing.Processors
{
    public interface IProcessor<T>
    {
        bool Process(T id);
    }
}
