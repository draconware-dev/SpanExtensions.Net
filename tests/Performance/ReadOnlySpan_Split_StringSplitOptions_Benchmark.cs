using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_Split_StringSplitOptions_Benchmark
    {
       
        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiterAndStringSplitOptions))]
        public int Split_StringSplitOptions_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, StringSplitOptions options)
        {
            int count = 0;

            foreach(ReadOnlySpan<char> part in value.Split(delimiter, options))
            {
                count += part.Length;
            }

            return count;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiterAndStringSplitOptions))]
        public int Split_StringSplitOptions_String(string value, char delimiter, StringSplitOptions options)
        {
            int count = 0;

            foreach(string part in value.Split(delimiter, options))
            {
                count += part.Length;
            }

            return count;
        }

        public IEnumerable<object[]> GetArgsWithDelimiterAndStringSplitOptions()
        {
            yield return ["xyz", 'y', StringSplitOptions.None];
            yield return ["abcde", 'c', StringSplitOptions.None];
            yield return ["abcdefg", 'd', StringSplitOptions.None];
            yield return ["abba", 'b', StringSplitOptions.None];
            yield return ["aaa", 'a', StringSplitOptions.None];
            yield return ["aba", 'a', StringSplitOptions.None];
            yield return ["12131415161718190", '1', StringSplitOptions.None];
            yield return [" xy z", 'y', StringSplitOptions.TrimEntries];
            yield return ["a bcd e", 'c', StringSplitOptions.TrimEntries];
            yield return [" ab cdef g ", 'd', StringSplitOptions.TrimEntries];
            yield return ["abb", 'b', StringSplitOptions.RemoveEmptyEntries];
            yield return ["aaa", 'a', StringSplitOptions.RemoveEmptyEntries];
            yield return ["aba   ", 'a', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
            yield return ["1 13141516171  190", '1', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        }
    }
}