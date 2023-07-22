using System.Text.Json;

namespace ParallelBatchProcessor.Runner.ProgressStorage
{
    public class ProgressFileStorage<TId> : IProgressStorage<TId>
    {
        private string FileName { get; }
        private string Delimiter { get; }

        private static object Lock = new object();

        public ProgressFileStorage(string filename)
        {
            FileName = filename;

            Delimiter = Convert.ToChar(999) + "\r\n"; // ϧ Coptic Small Letter Khei

            var path = new FileInfo(filename).DirectoryName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(FileName))
                File.WriteAllText(FileName, string.Empty);
        }

        public IEnumerable<TId> GetProcessed()
        {
            var textItems = File.ReadAllText(FileName)
                .Split(new string[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries);

            var processed = textItems.Select(txt => JsonSerializer.Deserialize<TId>(txt))
                .ToList();

            return processed;
        }

        public void MarkAsProcessed(TId id)
        {
            lock (Lock)
            {
                var text = JsonSerializer.Serialize(id);
                File.AppendAllText(FileName, text + Delimiter);
            }
        }
    }
}
