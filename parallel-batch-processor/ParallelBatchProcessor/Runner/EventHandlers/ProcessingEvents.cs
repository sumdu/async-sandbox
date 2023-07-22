using System;

namespace ParallelBatchProcessor.Runner.EventHandlers
{
    public delegate void ItemSuccessHandler<T>(T id);
    public delegate void ItemErrorHandler<T>(T id, Exception ex);
    public delegate void ItemCompeltedHandler<T>(double percentComplete);

    public delegate void ThreadCancelledHandler<T>();
    public delegate void ThreadCompletedHandler<T>();
    public delegate void ThreadStartedHandler<T>();

    public delegate void AllThreadsCompletedHandler<T>();

    public class ProcessingEvents<T>
    {
        public ItemSuccessHandler<T> OnItemSuccess { get; set; }
        public ItemErrorHandler<T> OnItemError { get; set; }

        public ItemCompeltedHandler<T> OnItemCompelted { get; set; }
        public ThreadCancelledHandler<T> OnThreadCancelled { get; set; }
        public ThreadCompletedHandler<T> OnThreadCompleted { get; set; }
        public ThreadStartedHandler<T> OnThreadStarted { get; set; }
        public AllThreadsCompletedHandler<T> OnAllThreadsCompleted { get; set; }
    }
}
