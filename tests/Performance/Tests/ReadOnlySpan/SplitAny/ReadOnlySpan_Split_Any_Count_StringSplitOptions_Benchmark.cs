using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_Split_Any_Count_StringSplitOptions_Benchmark
    {
        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_Count_ReadOnlySpan(ReadOnlySpan<char> value, char[] delimiters, int count, StringSplitOptions options)
        {
            int iterations = 0;

            foreach(ReadOnlySpan<char> part in value.SplitAny(delimiters, count, options))
            {
                iterations++;
            }

            return iterations;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_Count_String(string value, char[] delimiters, int count, StringSplitOptions options)
        {
            int iterations = 0;

            foreach(string part in value.Split(delimiters, count, options))
            {
                iterations++;
            }

            return iterations;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return ["abcde", new char[] { 'b', 'd' }, 2, StringSplitOptions.None];
            yield return ["abcdefg", new char[] { 'b', 'd', 'f' }, 4, StringSplitOptions.None];
            yield return ["abba", new char[] { 'b', 'a' }, 2, StringSplitOptions.None];
            yield return ["1234567890", new char[] { '1', '5', '7' }, 1, StringSplitOptions.None];
            yield return ["abcde", new char[] { 'b', 'c' }, 4, StringSplitOptions.RemoveEmptyEntries];
            yield return ["abba", new char[] { 'b', 'a' }, 1, StringSplitOptions.RemoveEmptyEntries];
            yield return ["abba", new char[] { 'b', 'a' }, 3, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries];
            yield return [" 1234 567890", new char[] { '1', '5', '7' }, 3, StringSplitOptions.TrimEntries];
        }
    }
}