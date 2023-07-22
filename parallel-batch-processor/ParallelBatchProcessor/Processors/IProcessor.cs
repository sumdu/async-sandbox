namespace ParallelBatchProcessor.Processors
{
    public interface IProcessor<T>
    {
        bool Process(T id);
    }
}
