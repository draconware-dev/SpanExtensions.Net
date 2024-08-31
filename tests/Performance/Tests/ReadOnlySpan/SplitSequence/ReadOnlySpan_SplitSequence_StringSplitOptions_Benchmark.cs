using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance;

[MemoryDiagnoser(false)]
public class ReadOnlySpan_SplitSequence_StringSplitOptions_Benchmark
{
    [Benchmark]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndStringSplitOptions))]
    public int Split_StringSplitOptions_ReadOnlySpan(ReadOnlySpan<char> value, ReadOnlySpan<char> delimiter, StringSplitOptions options)
    {
        int length = 0;

        foreach(ReadOnlySpan<char> part in value.Split(delimiter, options))
        {
            length += part.Length;
        }

        return length;
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetArgsWithDelimiterAndStringSplitOptions))]
    public int Split_StringSplitOptions_String(string value, string delimiter, StringSplitOptions options)
    {
        int length = 0;

        foreach(string part in value.Split(delimiter, options))
        {
            length += part.Length;
        }

        return length;
    }

    public IEnumerable<object[]> GetArgsWithDelimiterAndStringSplitOptions()
    {
        yield return ["xywz", "yw", StringSplitOptions.None];
        yield return ["abcde", "cde", StringSplitOptions.None];
        yield return ["abcdefg", "dde", StringSplitOptions.None];
        yield return ["abba", "bb", StringSplitOptions.None];
        yield return ["aaa", "aa", StringSplitOptions.None];
        yield return ["aba", "a", StringSplitOptions.None];
        yield return [string.Concat(Enumerable.Repeat("1213714151613718190", 12)), "37", StringSplitOptions.None];
        yield return [" wxy z", "xy", StringSplitOptions.TrimEntries];
        yield return ["a bcd e", "bcd", StringSplitOptions.TrimEntries];
        yield return [" ab cdef g ", "g", StringSplitOptions.TrimEntries];
        yield return ["abb", "bb", StringSplitOptions.RemoveEmptyEntries];
        yield return ["aaa", "aa", StringSplitOptions.RemoveEmptyEntries];
        yield return ["aba   ", "a", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
        yield return ["12 12312415161712  1290", "12", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries];
    }
}