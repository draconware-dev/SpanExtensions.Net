using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance;

[MemoryDiagnoser(false)]
public class ReadOnlySpan_SplitSequence_Count_Benchmark
{

    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_ReadOnlySpan(ReadOnlySpan<char> value, ReadOnlySpan<char> delimiter, int count)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count))
        {
            length += part.Length;
        }

        return length;
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_String(string value, string delimiter, int count)
    {
        int length = 0;

        foreach(string part in value.Split(delimiter, count))
        {
            length += part.Length;
        }

        return length;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_CutLastElements_ReadOnlySpan(ReadOnlySpan<char> value, ReadOnlySpan<char> delimiter, int count)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, CountExceedingBehaviour.CutLastElements))
        {
            length += part.Length;
        }

        return length;
    }

    public IEnumerable<object[]> GetArgsWithDelimiterAndCount()
    {
        yield return ["xywz", "yw", 1];
        yield return ["abcdef", "cde", 2];
        yield return ["abcdefg", "cde", 2];
        yield return ["abba", "bb", 9];
        yield return ["aaa", "aa", 1];
        yield return ["aba", "a", 2];
        yield return [string.Concat(Enumerable.Repeat("1213714151613718190", 12)), "37", 3];
    }
}