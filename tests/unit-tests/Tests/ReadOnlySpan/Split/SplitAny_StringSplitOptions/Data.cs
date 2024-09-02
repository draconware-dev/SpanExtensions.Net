using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitAny_StringSplitOptions
        {
            public static TheoryData<StringSplitOptions> StringSplitOptionsWithRemoveEmptyEntries
               => new TheoryData<StringSplitOptions>(stringSplitOptions.Where(x => x.HasFlag(StringSplitOptions.RemoveEmptyEntries)));
            public static TheoryData<StringSplitOptions> StringSplitOptionsWithoutRemoveEmptyEntries
               => new TheoryData<StringSplitOptions>(stringSplitOptions.Where(x => !x.HasFlag(StringSplitOptions.RemoveEmptyEntries)));
            public static TheoryData<StringSplitOptions> StringSplitOptions_Data
               => new TheoryData<StringSplitOptions>(stringSplitOptions);
            public static TheoryData<char[][], string, int, char[]> CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a', 'b', 'c', 'a', 'a'] }, "aabcaa", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'a', 'b', 'c', 'a', 'a'] }, "aabcaabcaa", 2, ['b', 'c'] }
                };
            public static TheoryData<string, char[], char[][], int> DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data
                => new TheoryData<string, char[], char[][], int>
                {
                    { "aab", ['b', 'c'], [['a', 'a', 'b']], 1 },
                    { "aacab", ['b', 'c'], [['a', 'a'], ['a', 'b']], 2 }
                };
            public static TheoryData<string, char[], char[][], int> CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data
                => new TheoryData<string, char[], char[][], int>
                {
                    { "aabaa", ['b', 'c'], [['a', 'a']], 1 },
                    { "aacabaa", ['b', 'c'], [['a', 'a'], ['a']], 2 }
                };
            public static TheoryData<StringSplitOptions, CountExceedingBehaviour, char[]> CountEqualZeroResultsInNothing_Data
                => new TheoryData<StringSplitOptions, CountExceedingBehaviour, char[]>
                {
                    { StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.RemoveEmptyEntries, CountExceedingBehaviour.CutRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.TrimEntries, CountExceedingBehaviour.CutRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries, CountExceedingBehaviour.CutRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.RemoveEmptyEntries, CountExceedingBehaviour.AppendRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.TrimEntries, CountExceedingBehaviour.AppendRemainingElements, ['a', 'b'] },
                    { StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries, CountExceedingBehaviour.AppendRemainingElements, ['a', 'b'] },
                };
            public static TheoryData<string, char[], char[][], int, CountExceedingBehaviour> CountEqualDelimiterCountResultsInSpanWithDelimiter_Data
                => new TheoryData<string, char[], char[][], int, CountExceedingBehaviour>
                {
                    { "aabaa", ['b', 'c'], [['a', 'a', 'b', 'a', 'a']], 1, CountExceedingBehaviour.AppendRemainingElements }
                };
        }
    }
}
