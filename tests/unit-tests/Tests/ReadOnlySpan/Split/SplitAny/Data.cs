namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitAny
        {
            public static TheoryData<char[][], string, int, char[]> CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a', 'b', 'a', 'a'] }, "aabaa", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'a', 'b', 'a', 'a'] }, "aacaabaa", 2, ['b', 'c'] }
                };
            public static TheoryData<char[][], string, int, char[]> DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a', 'b'] }, "aab", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'c'] }, "aabac", 2, ['b', 'c'] }
                };
            public static TheoryData<char[][], string, int, char[]> CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data
                => new TheoryData<char[][], string, int, char[]>
                {
                    { new char[][] { ['a', 'a'] }, "aabaa", 1, ['b', 'c'] },
                    { new char[][] { ['a', 'a'], ['a', 'a'] }, "aabaacaa", 2, ['b', 'c'] }
                };
        }
    }
}
