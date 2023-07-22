using System.Collections.Generic;

namespace ParallelBatchProcessor.Runner.ProgressStorage
{
    public interface IProgressStorage<TId>
    {
        IEnumerable<TId> GetProcessed();
        void MarkAsProcessed(TId id);
    }
}
