using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProcessing.Runner.EventHandlers
{


    public class ConsoleEventNotifier<T> : ProcessingEvents<T>
    {
        public ConsoleEventNotifier()
        {
            this.OnItemSuccess += ItemSuccess;
            this.OnItemError += ItemError;
            this.OnItemCompelted += ItemCompelted;

            this.OnThreadStarted += ThreadStarted;
            this.OnThreadCompleted += ThreadCompleted;
            this.OnThreadCancelled += ThreadCancelled;

            this.OnAllThreadsCompleted += AllThreadsCompleted;
        }

        public void ItemSuccess(T id)
        {
            // no need to polute console with messages
        }

        public void ItemError(T id, Exception ex)
        {
            Console.WriteLine($"Error processing {id}: {ex}");
        }

        public void ItemCompelted(double percentComplete)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is {percentComplete:0.##} % complete");
        }

        public void ThreadStarted()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
        }

        public void ThreadCompleted()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished");
        }

        public void ThreadCancelled()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is stopping");
        }

        public void AllThreadsCompleted()
        {
            Console.WriteLine("All threads are cancelled or completed");
        }
    }
}
