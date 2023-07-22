using System.Collections.Generic;

namespace ParallelBatchProcessor.Runner.ProgressStorage
{
    /// <summary>
    /// This class doesn't store progress. 
    /// It returns all items on every run
    /// </summary>
    public class StatelessSource<TId> : IProgressStorage<TId>
    {
        public IEnumerable<TId> GetProcessed()
        {
            return new TId[] { };
        }

        public void MarkAsProcessed(TId id)
        {
            // store nothing
        }
    }
}
