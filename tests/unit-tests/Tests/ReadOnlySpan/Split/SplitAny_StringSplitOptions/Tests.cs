using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class SplitAny_StringSplitOptions
        {
            [Theory]
            [MemberData(nameof(StringSplitOptionsWithoutRemoveEmptyEntries))]
            public void EmptySourceResultInEmptySpanUnless_StringSplitOptions_RemoveEmptyEntries_IsSet(StringSplitOptions options)
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = ReadOnlySpanExtensions.SplitAny(emptySpan, ['a', 'b'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void NoDelimiterOccurenceResultsInNoChange(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['c', 'd'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualZeroResultsInNothing_Data))]
            public void CountEqualZeroResultsInNothing(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour, char[] delimiters)
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, 0, options, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "abca";

                char[][] expected = [['a'], [], ['a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "abca";

                char[][] expected = [['a'], ['a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [[], ['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheStartWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithDelimiter(string input, char[] delimiters, char[][] expected, int count, CountExceedingBehaviour countExceedingBehaviour)
            {
                ReadOnlySpan<char> source = input;

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, count, StringSplitOptions.None, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(string input, char[] delimiters, char[][] expected, int count)
            {
                ReadOnlySpan<char> source = input;

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, count, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(string input, char[] delimiters, char[][] expected, int count)
            {
                ReadOnlySpan<char> source = input;

                var actual = ReadOnlySpanExtensions.SplitAny(source, delimiters, count, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 2, options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                ReadOnlySpan<char> source = "a b c a";

                char[][] expected = [['a'], [], ['a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                ReadOnlySpan<char> source = "\r \n  \t";

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['_', '!'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                ReadOnlySpan<char> source = "";

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "bcaa";

                char[][] expected = [['b', 'c', 'a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                ReadOnlySpan<char> source = " \t";

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " b\taa\n";

                char[][] expected = [['b', '\t', 'a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " b\taa\n";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.SplitAny(source, ['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void EmptyDelimiterSpanResultsSameAsCountEqualOne(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = " ";

                var expected = ReadOnlySpanExtensions.SplitAny(source, [], 1, options).ToSystemEnumerable();

                var actual = ReadOnlySpanExtensions.SplitAny(source, [], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => ReadOnlySpanExtensions.SplitAny("aabc".AsSpan(), ['b', 'c'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.SplitAny("aabc".AsSpan(), ['b', 'c'], 1, StringSplitOptions.None, InvalidCountExceedingBehaviour));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.SplitAny("aabc".AsSpan(), ['b', 'c'], InvalidStringSplitOptions));
            }
        }
    }
}
