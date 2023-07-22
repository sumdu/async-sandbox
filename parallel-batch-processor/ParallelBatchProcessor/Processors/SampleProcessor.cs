namespace ParallelBatchProcessor.Processors
{
    public class SampleProcessor : IProcessor<int>
    {
        public static object Lock = new object();

        public bool Process(int id)
        {
            lock (Lock)
            {
                Console.WriteLine($"Processing item {id}");
            }
            
            // or use a better way
            //await Console.Out.WriteLineAsync($"Processing item {id}");

            Task.Delay(1000);
            return true;
        }
    }
}
