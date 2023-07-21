using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelBatchProcessor.Runner.ProgressStorage
{
    public interface IItemSource<TId>
    {
        IEnumerable<TId> GetItemsToProcess();
        void MarkAsProcessed(TId id);
    }
}
