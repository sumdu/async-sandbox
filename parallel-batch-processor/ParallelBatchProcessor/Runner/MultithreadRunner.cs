using ParallelBatchProcessor.Runner.EventHandlers;

namespace ParallelBatchProcessor.Runner
{
    public class MultithreadRunner
    {
        public static void Run<TId>(int totalProcessedIdCount, IEnumerable<TId> ids, Func<TId, bool> processor, int threadCount, ProcessingEvents<TId> events,
            CancellationToken cancellationToken)
        {
            var workSplitter = new WorkSplitter();
            var tasks = workSplitter.CreateTasks(totalProcessedIdCount, cancellationToken, processor, ids, threadCount, events);
            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());
            events.OnAllThreadsCompleted();
        }

        public static void RunAsync<TId>(int totalProcessedIdCount, IEnumerable<TId> ids, Func<TId, Task<bool>> processor, int threadCount, ProcessingEvents<TId> events, 
            CancellationToken cancellationToken)
        {
            var workSplitter = new WorkSplitter();
            var tasks = workSplitter.CreateTasksAsync(totalProcessedIdCount, cancellationToken, processor, ids, threadCount, events);
            tasks.ForEach(t => t.Start());

            // see https://stackoverflow.com/questions/13046096/can-not-await-async-lambda
            Task.WaitAll(tasks.ToArray());
            var taskBodies = tasks.Select(t => t.Result);
            Task.WaitAll(taskBodies.ToArray());
            
            events.OnAllThreadsCompleted();
        }
    }
}
