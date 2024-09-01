namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitSequence
        {
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
