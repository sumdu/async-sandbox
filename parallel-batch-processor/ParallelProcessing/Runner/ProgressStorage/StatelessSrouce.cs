using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProcessing.Runner.ProgressStorage
{
    /// <summary>
    /// This class doesn't store progress. 
    /// It returns all items on every run
    /// </summary>
    public class StatelessSource<TId> : IItemSource<TId>
    {
        IEnumerable<TId> Ids;

        public StatelessSource(IEnumerable<TId> ids)
        {
            Ids = ids;
        }

        public IEnumerable<TId> GetItemsToProcess()
        {
            return Ids;
        }

        public void MarkAsProcessed(TId id)
        {
            // store nothing
        }
    }
}
