using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProcessing.Runner.EventHandlers
{
    public delegate void OnItemSuccessDelegate<T>(T id);
    public delegate void OnItemErrorDelegate<T>(T id, Exception ex);

    public interface IEventHandler<T>
    {
        OnItemSuccessDelegate<T> OnItemSuccess { get; set; }
        OnItemErrorDelegate<T> OnItemError { get; set; }

        void OnItemCompelted(double percentComplete);
        void OnThreadCancelled();
        void OnThreadCompleted();
        void OnThreadStarted();
        void OnAllThreadsCompleted();
    }
}
