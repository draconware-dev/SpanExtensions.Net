using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_Split_Any_Benchmark
    {
        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_ReadOnlySpan(ReadOnlySpan<char> value, char[] delimiters)
        {
            int iterations = 0;

            foreach(ReadOnlySpan<char> part in value.SplitAny(delimiters))
            {
                iterations++;
            }

            return iterations;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_String(string value, char[] delimiters)
        {
            int iterations = 0;

            foreach(string part in value.Split(delimiters))
            {
                iterations++;
            }

            return iterations;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return ["abcde", new char[] { 'b', 'd' }];
            yield return ["abcdefg", new char[] { 'b', 'd', 'f' }];
            yield return ["abba", new char[] { 'b', 'a' } ];
            yield return ["1234567890", new char[] { '1', '5', '7' }];
        }
    }
}