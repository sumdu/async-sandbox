using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelBatchProcessor.Extentions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Splits an array into several smaller arrays.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to split.</param>
        /// <param name="waysToSplit">Number of the smaller arrays to split to.</param>
        /// <returns>An array containing smaller arrays.</returns>
        public static IEnumerable<List<T>> Split<T>(this IEnumerable<T> array, int waysToSplit)
        {
            var batchSize = (int)Math.Ceiling((double)array.Count() / waysToSplit);
            for (var i = 0; i < waysToSplit; i++)
            {
                yield return array.Skip(i * batchSize).Take(batchSize).ToList();
            }
        }
    }
}
