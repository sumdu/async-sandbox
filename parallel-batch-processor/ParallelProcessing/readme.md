```cs
using System.Linq;
using System.Threading;
using ParallelBatchProcessor.Processors;

public class Demo : IProcessor<int>
{
  public void Run()
  {
    int threadCount = 3;

    // list of ids to process
    var ids = Enumerable.Range(1, 100).ToList();
    var processor = this;
    var progressFileName = "c:\\temp\\processed.txt";

    var consoleExecutor = new ConsoleExecutor();
    consoleExecutor.Process(threadCount, ids, processor, progressFileName);
  }

  /// <summary>
  /// Process one id
  /// </summary>
  /// <returns>
  /// True if success, False if failed. 
  /// Failed items will be retried on next run
  /// </returns>
  public bool Process(int id)
  {
    Thread.Sleep(1000);
    return true;
  }
}

```
