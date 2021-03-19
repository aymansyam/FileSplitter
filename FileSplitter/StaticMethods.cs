using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSplitter
{
    public static class StaticMethods
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, long chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
