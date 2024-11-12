using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class Split
        {
            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = ReadOnlySpanExtensions.Split(emptySpan, 'a').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, 'c').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [InlineData(CountExceedingBehaviour.AppendRemainingElements)]
            [InlineData(CountExceedingBehaviour.CutRemainingElements)]
            public void CountEqualZeroResultsInNothing(CountExceedingBehaviour countExceedingBehaviour)
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.Split(source, 'a', 0, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, 'a', 1).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [['a'], [], ['a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [[], ['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = ReadOnlySpanExtensions.Split(source, 'b').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => ReadOnlySpanExtensions.Split("aabb", 'b', -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.Split("aabb", 'b', 1, InvalidCountExceedingBehaviour));
            }
        }
    }
}
