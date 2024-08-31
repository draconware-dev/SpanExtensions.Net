using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance;

[MemoryDiagnoser(false)]
public class ReadOnlySpan_Split_Count_Benchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, int count)
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
    public int Split_String(string value, char delimiter, int count)
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
    public int Split_CutLastElements_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, int count)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, CountExceedingBehaviour.CutRemainingElements))
        {
            length += part.Length;
        }

        return length;
    }

    public IEnumerable<object[]> GetArgsWithDelimiterAndCount()
    {
        yield return ["xyz", 'y', 1];
        yield return ["aba", 'a', 2];
        yield return ["12131415161718190", '1', 3];
        yield return ["12131415161718190", '1', 2];
        yield return ["12131415161718190", '1', 7];
    }
}