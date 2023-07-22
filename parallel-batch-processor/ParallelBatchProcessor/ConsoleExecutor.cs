using ParallelBatchProcessor.Processors;
using ParallelBatchProcessor.Runner;
using ParallelBatchProcessor.Runner.EventHandlers;
using ParallelBatchProcessor.Runner.ProgressStorage;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ParallelBatchProcessor
{
    public class ConsoleExecutor
    {
        public void Process<T>(int threadCount, IList<T> ids, IProcessor<T> processor, IProgressStorage<T> progressStorage)
        {
            Process(threadCount, ids, null, processor, progressStorage);
        }

        public void Process<T>(int threadCount, IList<T> ids, IProcessorAsync<T> processor, IProgressStorage<T> progressStorage)
        {
            Process(threadCount, ids, processor, null, progressStorage);
        }

        public void Process<T>(int threadCount, IList<T> ids, IProcessorAsync<T> processorAsync, IProcessor<T> processor, IProgressStorage<T> progressStorage)
        {
            var cancellationSource = new CancellationTokenSource();

            var processed = progressStorage.GetProcessed();
            var idsToProcess = ids.Except(processed).ToList(); 

            var totalProcessedCount = ids.Count - idsToProcess.Count();

            // output progress to console
            var events = new ConsoleEventNotifier<T>();
            events.OnItemSuccess += (id) => progressStorage.MarkAsProcessed(id);

            Task t;
            if (processorAsync != null)
            {
                t = new Task(() =>
                {
                    MultithreadRunner.RunAsync(totalProcessedCount, idsToProcess, processorAsync.ProcessAsync, threadCount, events, cancellationSource.Token);
                });
            }
            else if (processor != null)  
            {
                t = new Task(() =>
                {
                    MultithreadRunner.Run(totalProcessedCount, idsToProcess, processor.Process, threadCount, events, cancellationSource.Token);
                });
            }
            else
            {
                throw new ArgumentNullException("Please, provide either processorAsync or processor argument");
            }

            t.Start();

            // we need to keep main thread alive
            Console.WriteLine("Hit <Enter> to stop...");
            Console.ReadLine();

            if (!t.IsCompleted)
            {
                cancellationSource.Cancel();
                Console.ReadLine();
            }
        }
    }
}
