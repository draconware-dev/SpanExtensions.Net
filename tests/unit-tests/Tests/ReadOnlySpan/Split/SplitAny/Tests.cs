using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitAny
        {
            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = ReadOnlySpanExtensions.SplitAny(emptySpan, ['a', 'b']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['c', 'd']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.SplitAny(source, []).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [InlineData(CountExceedingBehaviour.AppendRemainingElements)]
            [InlineData(CountExceedingBehaviour.CutRemainingElements)]
            public void CountEqualZeroResultsInNothing(CountExceedingBehaviour countExceedingBehaviour)
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['a', 'b'], 0, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['a', 'b'], 1).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [['a'], [], ['a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [[], ['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter(char[][] expected, string sourceString, int count, char[] delimiters)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(char[][] expected, string sourceString, int count, char[] delimiters)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(char[][] expected, string sourceString, int count, char[] delimiters)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, count, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => ReadOnlySpanExtensions.SplitAny("aabc".AsSpan(), ['b', 'c'], -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.SplitAny("aabb".AsSpan(), ['b', 'c'], 1, InvalidCountExceedingBehaviour));
            }
        }
    }
}
