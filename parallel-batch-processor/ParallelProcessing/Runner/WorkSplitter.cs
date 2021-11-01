using ParallelProcessing.Extentions;
using ParallelProcessing.Runner.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProcessing.Runner
{
    class WorkSplitter
    {
        public List<Task> CreateTasks<TId>(int totalProcessedIdCount, CancellationToken cancellationToken, Func<TId, bool> processor, IEnumerable<TId> idsToProcess, 
            int threadCount, IEventHandler<TId> eventHandler)
        {
            var tasks = new List<Task>();
            var listOfLists = idsToProcess.Split(threadCount);
            foreach (var part in listOfLists)
            {
                // start a task for Nth subset
                var task = CreateTaskForBatch(totalProcessedIdCount / threadCount, part, processor, cancellationToken, eventHandler);
                tasks.Add(task);
            }

            return tasks;
        }

        public Task CreateTaskForBatch<TId>(int totalProcessedPerThread, IList<TId> idsToProcess, Func<TId, bool> processor, CancellationToken cancellationToken,
            IEventHandler<TId> eventHandler)
        {
            return new Task(() =>
            {
                eventHandler.OnThreadStarted();

                int i = 0;
                foreach (var id in idsToProcess)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        eventHandler.OnThreadCancelled();
                        break;
                    }

                    bool success;
                    try
                    {
                        success = processor(id);
                        eventHandler.OnItemSuccess(id);
                    }
                    catch (Exception ex)
                    {
                        eventHandler.OnItemError(id, ex);
                    }
                    finally
                    {
                        i++;
                        var percentComplete = 100.0 * (i + totalProcessedPerThread) / (totalProcessedPerThread + idsToProcess.Count);
                        eventHandler.OnItemCompelted(percentComplete);
                    }
                }

                eventHandler.OnThreadCompleted();
            });
        }
    }
}
