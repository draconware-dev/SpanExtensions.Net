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
            int length = 0;

            foreach(ReadOnlySpan<char> part in value.SplitAny(delimiters, count, options))
            {
                length += part.Length;
            }

            return length;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_Any_Count_String(string value, char[] delimiters, int count, StringSplitOptions options)
        {
            int length = 0;

            foreach(string part in value.Split(delimiters, count, options))
            {
                length += part.Length;
            }

            return length;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return ["abcde", new object[] { 'b', 'd' }, 2, StringSplitOptions.None];
            yield return ["abcdefg", new object[] { 'b', 'd', 4, 'f' }, StringSplitOptions.None];
            yield return ["abba", new object[] { 'b', 'a' }, 2, StringSplitOptions.None];
            yield return ["1234567890", new object[] { '1', '5', '7' }, 1, StringSplitOptions.None];
            yield return ["abcde", new object[] { 'b', 'c' }, 4, StringSplitOptions.RemoveEmptyEntries];
            yield return ["abba", new object[] { 'b', 'a' }, 1, StringSplitOptions.RemoveEmptyEntries];
            yield return ["abba", new object[] { 'b', 'a' }, 3, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries];
            yield return [" 1234 567890", new object[] { '1', '5', '7' }, 3, StringSplitOptions.TrimEntries];
        }
    }
}