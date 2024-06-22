
namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class Split_StringSplitOptions
        {
            public static TheoryData<StringSplitOptions> StringSplitOptionsWithRemoveEmptyEntries
               => (TheoryData<StringSplitOptions>)stringSplitOptions.Where(x => x.HasFlag(StringSplitOptions.RemoveEmptyEntries));
            public static TheoryData<StringSplitOptions> StringSplitOptions_Data
               => (TheoryData<StringSplitOptions>)stringSplitOptions;
            public static TheoryData<StringSplitOptions, CountExceedingBehaviour, char> CountEqualZeroResultsInNothing_Data
                => new TheoryData<StringSplitOptions, CountExceedingBehaviour, char>
                {
                    { StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements, 'a' },
                    { StringSplitOptions.RemoveEmptyEntries, CountExceedingBehaviour.CutRemainingElements, 'a' },
                    { StringSplitOptions.TrimEntries, CountExceedingBehaviour.CutRemainingElements, 'a' },
                    { StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries, CountExceedingBehaviour.CutRemainingElements , 'a' },
                    { StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements, 'a' },
                    { StringSplitOptions.RemoveEmptyEntries, CountExceedingBehaviour.AppendRemainingElements, 'a' },
                    { StringSplitOptions.TrimEntries, CountExceedingBehaviour.AppendRemainingElements, 'a' },
                    { StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries, CountExceedingBehaviour.AppendRemainingElements, 'a' },
                };
            public static TheoryData<char[][], string, int, char[]> CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a', 'b', 'c', 'a', 'a'] }, "aabcaa", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'a', 'b', 'c', 'a', 'a'] }, "aabcaabcaa", 2, ['b', 'c'] }
                };
            public static TheoryData<char[][], string, int, char[]> DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a', 'b', 'c'] }, "aabc", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'b', 'c'] }, "aabcabc", 2, ['b', 'c'] }
                };
            public static TheoryData<char[][], string, int, char[]> CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a'] }, "aabcaa", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'a'] }, "aabcaabcaa", 2, ['b', 'c'] }
                };
        }
    }
}
