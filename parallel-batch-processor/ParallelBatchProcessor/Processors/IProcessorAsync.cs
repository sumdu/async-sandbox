namespace ParallelBatchProcessor.Processors
{
    public interface IProcessorAsync<T>
    {
        Task<bool> ProcessAsync(T id);
    }
}
