using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance;

[MemoryDiagnoser(false)]
public class ReadOnlySpan_Split_Count_Benchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, int count)
    {
        int iterations = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count))
        {
            iterations++;
        }

        return iterations;
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_String(string value, char delimiter, int count)
    {
        int iterations = 0;

        foreach(string part in value.Split(delimiter, count))
        {
            iterations++;
        }

        return iterations;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCount))]
    public int Split_CutLastElements_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, int count)
    {
        int iterations = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, CountExceedingBehaviour.CutRemainingElements))
        {
            iterations++;
        }

        return iterations;
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