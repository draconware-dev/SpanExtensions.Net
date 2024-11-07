using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_Split_Any_Count_Benchmark
    {
        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_Count_ReadOnlySpan(ReadOnlySpan<char> value, char[] delimiters, int count)
        {
            int iterations = 0;

            foreach(ReadOnlySpan<char> part in ReadOnlySpanExtensions.SplitAny(value, delimiters, count))
            {
                iterations++;
            }

            return iterations;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_Count_String(string value, char[] delimiters, int count)
        {
            int iterations = 0;

            foreach(string part in value.Split(delimiters, count))
            {
                iterations++;
            }

            return iterations;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return ["abcde", new char[] { 'b', 'd' }, 2];
            yield return ["abcdefg", new char[] { 'b', 'd', 'f' }, 4];
            yield return ["abba", new char[] { 'b', 'a' }, 2];
            yield return ["1234567890", new char[] { '1', '5', '7' }, 1];
        }
    }
}