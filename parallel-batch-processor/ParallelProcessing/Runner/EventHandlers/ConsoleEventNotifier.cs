using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProcessing.Runner.EventHandlers
{
    public class ConsoleEventNotifier<T> : IEventHandler<T>
    {
        OnItemSuccessDelegate<T> IEventHandler<T>.OnItemSuccess { get; set; }
        OnItemErrorDelegate<T> IEventHandler<T>.OnItemError { get; set; }
        
        public void OnItemCompelted(double percentComplete)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is {percentComplete:0.##} % complete");
        }

        public void OnAllThreadsCompleted()
        {
            Console.WriteLine("All threads are cancelled or completed");
        }

        public void OnThreadStarted()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
        }

        public void OnThreadCompleted()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished");
        }

        public void OnThreadCancelled()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is stopping");
        }
    }
}
