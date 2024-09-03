using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitStringSequence
        {
            [Theory]
            [MemberData(nameof(StringSplitOptionsWithoutRemoveEmptyEntries))]
            public void EmptySourceResultInEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = emptySpan.Split(['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void NoDelimiterOccurenceResultsInNoChange(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.Split(['c', 'd'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.Split([], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualZeroResultsInNothing_Data))]
            public void CountEqualZeroResultsInNothing(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour, char[] delimiter)
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [];

                var actual = source.Split(delimiter, 0, options, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.Split(['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "abcbca";

                char[][] expected = [['a'], [], ['a']];

                var actual = source.Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "abcbca";

                char[][] expected = [['a'], ['a']];

                var actual = source.Split(['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "bcaa";

                char[][] expected = [[], ['a', 'a']];

                var actual = source.Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheStartWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "bcaa";

                char[][] expected = [['a', 'a']];

                var actual = source.Split(['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['a', 'a'], []];

                var actual = source.Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['a', 'a']];

                var actual = source.Split(['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter(char[][] expected, string sourceString, int count, char[] delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = source.Split(delimiter, count, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(char[][] expected, string sourceString, int count, char[] delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = source.Split(delimiter, count, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(char[][] expected, string sourceString, int count, char[] delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = source.Split(delimiter, count, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aabcbc";

                char[][] expected = [['a', 'a']];

                var actual = source.Split(['b', 'c'], 2, options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void TrimEntriesOptionTrimsEverySpan()
            {
                ReadOnlySpan<char> source = " a\tbc\na\r";

                char[][] expected = [['a'], ['a']];

                var actual = source.Split(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                ReadOnlySpan<char> source = " \t";

                char[][] expected = [];

                var actual = source.Split(['b', 'c'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                ReadOnlySpan<char> source = "aabcbc";

                char[][] expected = [['a', 'a']];

                var actual = source.Split(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                ReadOnlySpan<char> source = "";

                char[][] expected = [];

                var actual = source.Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "bcaa";

                char[][] expected = [['b', 'c', 'a', 'a']];

                var actual = source.Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "bcaa";

                char[][] expected = [['a', 'a']];

                var actual = source.Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                ReadOnlySpan<char> source = " \t";

                char[][] expected = [];

                var actual = source.Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " bc\taa\n";

                char[][] expected = [['b', 'c', '\t', 'a', 'a']];

                var actual = source.Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " bc\taa\n";

                char[][] expected = [['a', 'a']];

                var actual = source.Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().Split(['b', 'c'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, InvalidCountExceedingBehaviour));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().Split(['b', 'c'], InvalidStringSplitOptions));
            }
        }
    }
}
