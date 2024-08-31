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
            int length = 0;

            foreach(ReadOnlySpan<char> part in value.SplitAny(delimiters, count))
            {
                length += part.Length;
            }

            return length;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_Count_String(string value, char[] delimiters, int count)
        {
            int length = 0;

            foreach(string part in value.Split(delimiters, count))
            {
                length += part.Length;
            }

            return length;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return ["abcde", new object[] { 'b', 'd' }, 2];
            yield return ["abcdefg", new object[] { 'b', 'd', 'f' }, 4];
            yield return ["abba", new object[] { 'b', 'a' }, 2];
            yield return ["1234567890", new object[] { '1', '5', '7' }, 1];
        }
    }
}