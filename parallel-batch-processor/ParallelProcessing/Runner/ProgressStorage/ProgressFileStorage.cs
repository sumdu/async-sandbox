using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ParallelProcessing.Runner.ProgressStorage
{
    public class ProgressFileStorage<TId> : IItemSource<TId>
    {
        private string FileName { get; }
        private string Delimiter { get; }

        private IList<TId> ItemsToProcess;

        private static object Lock = new object();

        public ProgressFileStorage(string filename, IList<TId> itemsToProcess)
        {
            FileName = filename;
            ItemsToProcess = itemsToProcess;

            Delimiter = Convert.ToChar(999) + "\r\n"; // 	ϧ	Coptic Small Letter Khei

            var path = new FileInfo(filename).DirectoryName;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(FileName))
                File.WriteAllText(FileName, string.Empty);
        }

        public IEnumerable<TId> GetItemsToProcess()
        {
            var textItems = File.ReadAllText(FileName)
                .Split(new string[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries);

            var processed = textItems.Select(txt => JsonSerializer.Deserialize<TId>(txt))
                .ToList();

            return ItemsToProcess.Except(processed).ToList();
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
