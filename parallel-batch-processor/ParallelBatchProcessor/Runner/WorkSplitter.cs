using ParallelBatchProcessor.Extentions;
using ParallelBatchProcessor.Runner.EventHandlers;

namespace ParallelBatchProcessor.Runner
{
    class WorkSplitter
    {
        public List<Task> CreateTasks<TId>(int totalProcessedIdCount, CancellationToken cancellationToken, Func<TId, bool> processor,
            IEnumerable<TId> idsToProcess, int threadCount, ProcessingEvents<TId> events)
        {
            var tasks = new List<Task>();
            var listOfLists = idsToProcess.Split(threadCount);
            foreach (var part in listOfLists)
            {
                // start a task for Nth subset
                var task = CreateTaskForBatch(totalProcessedIdCount / threadCount, part, processor, cancellationToken, events);
                tasks.Add(task);
            }

            return tasks;
        }

        public Task CreateTaskForBatch<TId>(int totalProcessedPerThread, IList<TId> idsToProcess, Func<TId, bool> processor,
            CancellationToken cancellationToken, ProcessingEvents<TId> events)
        {
            return new Task(() =>
            {
                events.OnThreadStarted();

                int i = 0;
                foreach (var id in idsToProcess)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        events.OnThreadCancelled();
                        break;
                    }

                    bool success;
                    try
                    {
                        success = processor(id);
                        events.OnItemSuccess(id);
                    }
                    catch (Exception ex)
                    {
                        events.OnItemError(id, ex);
                    }
                    finally
                    {
                        i++;
                        var percentComplete = 100.0 * (i + totalProcessedPerThread) / (totalProcessedPerThread + idsToProcess.Count);
                        events.OnItemCompelted(percentComplete);
                    }
                }

                events.OnThreadCompleted();
            });
        }

        public List<Task<Task>> CreateTasksAsync<TId>(int totalProcessedIdCount, CancellationToken cancellationToken, Func<TId, Task<bool>> processor, 
            IEnumerable<TId> idsToProcess, int threadCount, ProcessingEvents<TId> events)
        {
            var tasks = new List<Task<Task>>();
            var listOfLists = idsToProcess.Split(threadCount);
            foreach (var part in listOfLists)
            {
                // start a task for Nth subset
                var task = CreateTaskForBatchAsync(totalProcessedIdCount / threadCount, part, processor, cancellationToken, events);
                tasks.Add(task);
            }

            return tasks;
        }

        public Task<Task> CreateTaskForBatchAsync<TId>(int totalProcessedPerThread, IList<TId> idsToProcess, Func<TId, Task<bool>> processor, 
            CancellationToken cancellationToken, ProcessingEvents<TId> events)
        {
            return new Task<Task>(async () =>
            {
                events.OnThreadStarted();

                int i = 0;
                foreach (var id in idsToProcess)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        events.OnThreadCancelled();
                        break;
                    }

                    bool success;
                    try
                    {
                        success = await processor(id);
                        events.OnItemSuccess(id);
                    }
                    catch (Exception ex)
                    {
                        events.OnItemError(id, ex);
                    }
                    finally
                    {
                        i++;
                        var percentComplete = 100.0 * (i + totalProcessedPerThread) / (totalProcessedPerThread + idsToProcess.Count);
                        events.OnItemCompelted(percentComplete);
                    }
                }

                events.OnThreadCompleted();
            });
        }
    }
}
