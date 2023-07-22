using System.Threading.Tasks;

namespace ParallelBatchProcessor.Processors
{
    public interface IProcessorAsync<T>
    {
        /// <summary>
        /// Implement logic for processing one record
        /// </summary>
        /// <returns>
        /// `True` if success, `False` if failed. 
        /// Failed items will be not persisted to `IProgressStorage<T>` as processed. 
        /// They will be processed again on next run.
        /// </returns>
        Task<bool> ProcessAsync(T id);
    }
}