namespace ParallelBatchProcessor.Processors
{
    public class SampleProcessorAsync : IProcessorAsync<int>
    {
        public static object Lock = new object();

        public async Task<bool> ProcessAsync(int id)
        {
            lock (Lock)
            {
                Console.WriteLine($"Processing item {id}");
            }
            
            // or use a better way
            //await Console.Out.WriteLineAsync($"Processing item {id}");

            await Task.Delay(1000);
            return true;
        }
    }
}
