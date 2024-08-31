using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance;

[MemoryDiagnoser(false)]
public class ReadOnlySpan_Split_Count_StringSplitOptions_Benchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCountAndStringSplitOptions))]
    public int Split_Count_StringSplitOptions_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, int count, StringSplitOptions options)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, options))
        {
            length += part.Length;
        }

        return length;
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCountAndStringSplitOptions))]
    public int Split_Count_StringSplitOptions_String(string value, char delimiter, int count, StringSplitOptions options)
    {
        int length = 0;

        foreach(string part in value.Split(delimiter, count, options))
        {
            length += part.Length;
        }

        return length;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndCountAndStringSplitOptions))]
    public int Split_Count_StringSplitOptions_CountExceedingBehaviour_CutLastElements_ReadOnlySpan(ReadOnlySpan<char> value, char delimiter, int count, StringSplitOptions options)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, count, options, CountExceedingBehaviour.CutRemainingElements))
        {
            length += part.Length;
        }

        return length;
    }

    public IEnumerable<object[]> GetArgsWithDelimiterAndCountAndStringSplitOptions()
    {
        yield return new object[] { "12131415161718190", '1', 3, StringSplitOptions.None };
        yield return new object[] { "12131415161718190", '1', 2, StringSplitOptions.None };
        yield return ["abba", 'b', 4, StringSplitOptions.None];
        yield return ["aaa", 'a', 1, StringSplitOptions.None];
        yield return ["aba", 'a', 10, StringSplitOptions.None];
        yield return ["12131415161718190", '1', 12, StringSplitOptions.None];
        yield return [" xy z", 'y', 2, StringSplitOptions.TrimEntries];
        yield return ["a bcd e", 'c', 1, StringSplitOptions.TrimEntries];
        yield return [" ab cdef g ", 'd', 4, StringSplitOptions.TrimEntries];
        yield return ["abb", 'b', 1, StringSplitOptions.RemoveEmptyEntries];
        yield return ["abb", 'b', 2, StringSplitOptions.RemoveEmptyEntries];
        yield return ["aaa", 'a', 1, StringSplitOptions.RemoveEmptyEntries];
        yield return ["aaa", 'a', 2, StringSplitOptions.RemoveEmptyEntries];
        yield return ["aba   ", 'a', 1, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        yield return ["aba   ", 'a', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        yield return ["aba   ", 'a', 3, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        yield return ["1 13141516171  190", '1', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        yield return ["1 131 151 171  190", '1', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        yield return ["1131151171 190", '1', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
    }
}