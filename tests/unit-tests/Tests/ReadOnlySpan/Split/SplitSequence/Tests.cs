using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitSequence
        {
            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = ReadOnlySpanExtensions.Split(emptySpan, ['a', 'b']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, ['c', 'd']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, []).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [InlineData(CountExceedingBehaviour.AppendRemainingElements)]
            [InlineData(CountExceedingBehaviour.CutRemainingElements)]
            public void CountEqualZeroResultsInNothing(CountExceedingBehaviour countExceedingBehaviour)
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.Split(source, ['a', 'b'], 0, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, ['a', 'b'], 1).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "abcbca";

                char[][] expected = [['a'], [], ['a']];

                var actual = ReadOnlySpanExtensions.Split(source, ['b', 'c']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "bcaa";

                char[][] expected = [[], ['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, ['b', 'c']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['a', 'a'], []];

                var actual = ReadOnlySpanExtensions.Split(source, ['b', 'c']).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter(char[][] expected, string sourceString, int count, char[] delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(char[][] expected, string sourceString, int count, char[] delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(char[][] expected, string sourceString, int count, char[] delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => ReadOnlySpanExtensions.Split("aabcbc".AsSpan(), ['b', 'c'], -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.Split("aabcbc".AsSpan(), ['b', 'c'], 1, InvalidCountExceedingBehaviour));
            }
        }
    }
}
