﻿using ParallelBatchProcessor.Runner.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    }
}