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
            int length = 0;

            foreach(ReadOnlySpan<char> part in value.SplitAny(delimiters))
            {
                length += part.Length;
            }

            return length;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_String(string value, char[] delimiters)
        {
            int length = 0;

            foreach(string part in value.Split(delimiters))
            {
                length += part.Length;
            }

            return length;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return new object[] { "abcde", new object[] { 'b', 'd' } };
            yield return new object[] { "abcdefg", new object[] { 'b', 'd', 'f' } };
            yield return new object[] { "abba", new object[] { 'b', 'a' } };
            yield return new object[] { "1234567890", new object[] { '1', '5', '7' } };
        }
    }
}