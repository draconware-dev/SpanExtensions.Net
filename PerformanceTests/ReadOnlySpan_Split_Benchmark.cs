using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_Split_Benchmark
    {

        [Benchmark]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter)
        {
            int count = 0;
            foreach(ReadOnlySpan<char> part in value.Split(delimiter))
            {
                count += part.Length;
            }
            return count;
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetArgsWithDelimiter))]
        public int Split_String(string value, char delimiter)
        {
            return value.Split(delimiter).Length;
        }

        public IEnumerable<object[]> GetArgsWithDelimiter()
        {
            yield return new object[] { "xyz", 'y' };
            yield return new object[] { "abcde", 'c' };
            yield return new object[] { "abcdefg", 'd' };
            yield return new object[] { "abba", 'b' };
            yield return new object[] { "aaa", 'a' };
            yield return new object[] { "aba", 'a' };
            yield return new object[] { "12131415161718190", '1' };
        }
    } 
}