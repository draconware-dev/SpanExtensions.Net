using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance;

[MemoryDiagnoser(false)]
public class ReadOnlySpan_SplitSequence_Benchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiter))]
    public int Split_ReadOnlySpan(ReadOnlySpan<char> value, ReadOnlySpan<char> delimiter)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter))
        {
            length += part.Length;
        }

        return length;
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetArgsWithDelimiter))]
    public int Split_String(string value, string delimiter)
    {
        int length = 0;

        foreach(string part in value.Split(delimiter))
        {
            length += part.Length;
        }

        return length;
    }

    public IEnumerable<object[]> GetArgsWithDelimiter()
    {
        yield return ["xywz", "yw"];
        yield return ["abcdef", "cde"];
        yield return ["abcdefg", "cde"];
        yield return ["abba", "bb"];
        yield return ["aaa", "aa"];
        yield return ["aba", "a"];
        yield return [string.Concat(Enumerable.Repeat("1213714151613718190", 12)), "37"];
    }
}