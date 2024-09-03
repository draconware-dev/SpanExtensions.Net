using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_SplitSequence_Count_StringSplitOptions_Benchmark
    {
        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiterAndCountAndStringSplitOptions))]
        public int Split_Count_StringSplitOptions_ReadOnlySpan(ReadOnlySpan<char> value, ReadOnlySpan<char> delimiter, int count, StringSplitOptions options)
        {
            int iterations = 0;

            foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, options))
            {
                iterations++;
            }

            return iterations;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiterAndCountAndStringSplitOptions))]
        public int Split_Count_StringSplitOptions_String(string value, string delimiter, int count, StringSplitOptions options)
        {
            int iterations = 0;

            foreach(string part in value.Split(delimiter, count, options))
            {
                iterations++;
            }

            return iterations;
        }

        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiterAndCountAndStringSplitOptions))]
        public int Split_Count_StringSplitOptions_CountExceedingBehaviour_CutLastElements_ReadOnlySpan(ReadOnlySpan<char> value, ReadOnlySpan<char> delimiter, int count, StringSplitOptions options)
        {
            int iterations = 0;

            foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, options, CountExceedingBehaviour.CutRemainingElements))
            {
                iterations++;
            }

            return iterations;
        }

        public IEnumerable<object[]> GetArgsWithDelimiterAndCountAndStringSplitOptions()
        {
            yield return ["xywz", "yw", 1, StringSplitOptions.None];
            yield return ["abcde", "cde", 2, StringSplitOptions.None];
            yield return ["abcdefg", "dde", 2, StringSplitOptions.None];
            yield return ["abba", "bb", 9, StringSplitOptions.None];
            yield return ["aaa", "aa", 1, StringSplitOptions.None];
            yield return ["aba", "a", 2, StringSplitOptions.None];
            yield return [string.Concat(Enumerable.Repeat("1213714151613718190", 12)), "37", 12, StringSplitOptions.None];
            yield return [" wxy z", "xy", 3, StringSplitOptions.TrimEntries];
            yield return ["a bcd e", "bcd", 4, StringSplitOptions.TrimEntries];
            yield return [" ab cdef g ", "g", 1, StringSplitOptions.TrimEntries];
            yield return ["abb", "bb", 3, StringSplitOptions.RemoveEmptyEntries];
            yield return ["aaa", "aa", 2, StringSplitOptions.RemoveEmptyEntries];
            yield return ["aba   ", "a", 3, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
            yield return ["12 12312415161712  1290", "12", 7, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        }
    }
}