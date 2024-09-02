namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class Split
        {
            public static TheoryData<char[][], string, int, char> CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data
                => new TheoryData<char[][], string, int, char>
                {
                    { new char[][] { ['a', 'a', 'b', 'a', 'a'] }, "aabaa", 1, 'b' },
                    { new char[][] { ['a', 'a'], ['a', 'a', 'b', 'a', 'a'] }, "aabaabaa", 2, 'b' }
                };
            public static TheoryData<char[][], string, int, char> DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data
                => new TheoryData<char[][], string, int, char>
                {
                    { new char[][] { ['a', 'a', 'b'] }, "aab", 1, 'b' },
                    { new char[][] { ['a', 'a'], ['a', 'b'] }, "aabab", 2, 'b' }
                };
            public static TheoryData<char[][], string, int, char> CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data
                => new TheoryData<char[][], string, int, char>
                {
                    { new char[][] { ['a', 'a'] }, "aabaa", 1, 'b' },
                    { new char[][] { ['a', 'a'], ['a', 'a'] }, "aabaabaa", 2, 'b' }
                };
        }
    }
}
