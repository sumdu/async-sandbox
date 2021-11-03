using ParallelBatchProcessor.Processors;
using ParallelBatchProcessor.Runner;
using ParallelBatchProcessor.Runner.EventHandlers;
using ParallelBatchProcessor.Runner.ProgressStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelBatchProcessor
{
    public class ConsoleExecutor
    {
        public void Process<T>(int threadCount, IList<T> ids, IProcessor<T> processor, string processedFileName)
        {
            var cancellationSource = new CancellationTokenSource();

            var progressStorage = new ProgressFileStorage<T>(processedFileName, ids);
            var idsToProcess = progressStorage.GetItemsToProcess();
            var totalProcessedCount = ids.Count - idsToProcess.Count();

            // output progress to console
            var events = new ConsoleEventNotifier<T>();
            events.OnItemSuccess += (id) => progressStorage.MarkAsProcessed(id);

            var t = new Task(() =>
            {
                MultithreadRunner.Run(totalProcessedCount, idsToProcess, processor.Process, threadCount, events, cancellationSource.Token);
            });
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
