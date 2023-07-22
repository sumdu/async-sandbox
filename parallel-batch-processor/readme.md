# What does it do?

A library that makes it easy for developes to process a list of items in parallel in a Console application.
Source code can be enhanced to use it in other types of applications, but there are no classes to support it out-of-the-box yet.

# Usage Examples

Example 1: Synchronous method to process one record

```cs
using System.Linq;
using System.Threading;
using ParallelBatchProcessor;
using ParallelBatchProcessor.Processors;
using ParallelBatchProcessor.Runner.ProgressStorage;

public class Demo : IProcessor<int>
{
    public void Run()
    {
        // list of ids to process
        var ids = Enumerable.Range(1, 100).ToList();

        int threadCount = 3;
        var processor = this;
        var progressFileName = "c:\\temp\\processed.txt";
        var progressStorage = new ProgressFileStorage<int>(progressFileName);

        var consoleExecutor = new ConsoleExecutor();
        consoleExecutor.Process(threadCount, ids, processor, progressStorage);
    }

    /// <summary>
    /// Implement logic for processing one record
    /// </summary>
    /// <returns>
    /// `True` if success, `False` if failed. 
    /// Failed items will be not persisted to `IProgressStorage<T>` as processed. 
    /// They will be processed again on next run.
    /// </returns>
    public bool Process(int id)
    {
        Thread.Sleep(1000);
        return true;
    }
}
```

Example 2: Asynchronous method to process one record

```cs
using ParallelBatchProcessor;
using ParallelBatchProcessor.Processors;
using ParallelBatchProcessor.Runner.ProgressStorage;
using System.Linq;
using System.Threading.Tasks;

public class Demo : IProcessorAsync<int>
{
    public void Run()
    {
        // list of ids to process
        var ids = Enumerable.Range(1, 100).ToList();

        int threadCount = 3;
        var processor = this;
        var progressFileName = "c:\\temp\\processed.txt";
        var progressStorage = new ProgressFileStorage<int>(progressFileName);

        var consoleExecutor = new ConsoleExecutor();
        consoleExecutor.Process(threadCount, ids, processor, progressStorage);
    }

    /// <summary>
    /// Implement logic for processing one record
    /// </summary>
    /// <returns>
    /// `True` if success, `False` if failed. 
    /// Failed items will be not persisted to `IProgressStorage<T>` as processed. 
    /// They will be processed again on next run.
    /// </returns>
    public async Task<bool> ProcessAsync(int id)
    {
        await Task.Delay(1000);
        return true;
    }
}
```

## Version History

Version 2.0.0:

* Upgraded to target .NET Core 3.1 Framework
* Added support for async functions to process a one record

Version 1.0.1:

* First public version
* Supports only sync functions to process a one record
* .NET 4.8 Framework
