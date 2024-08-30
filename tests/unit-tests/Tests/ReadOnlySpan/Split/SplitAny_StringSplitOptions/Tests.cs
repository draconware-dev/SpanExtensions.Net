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

                var actual = emptySpan.SplitAny(['a', 'b'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void NoDelimiterOccurenceResultsInNoChange(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.SplitAny(['c', 'd'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void WhiteSpaceCharactersAssumedWhenDelimitersSpanIsEmpty(StringSplitOptions options)
            {
                ReadOnlySpan<char> emptySpan = "a b c d";

                var expected = new char[][] { ['a'], ['b'], ['c'], ['d'] };

                var actual = emptySpan.SplitAny([], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualZeroResultsInNothing_Data))]
            public void CountEqualZeroResultsInNothing(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour, char[] delimiters)
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = EmptyNestedCharArray;

                var actual = source.SplitAny(delimiters, 0, options, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.SplitAny(['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "abca";

                char[][] expected = [['a'], [], ['a']];

                var actual = source.SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "abca";

                char[][] expected = [['a'], ['a']];

                var actual = source.SplitAny(['b', 'c'], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [[], ['a', 'a']];

                var actual = source.SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheStartWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['a'], ['a']];

                var actual = source.SplitAny(['b', 'c'], 1, options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = source.SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = source.SplitAny(['b', 'c'], 1, options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithDelimiter(string input, char[] delimiters, char[][] expected, int count, CountExceedingBehaviour countExceedingBehaviour)
            {
                ReadOnlySpan<char> source = input;

                var actual = source.SplitAny(delimiters, count, StringSplitOptions.None, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(string input, char[] delimiters, char[][] expected, int count)
            {
                ReadOnlySpan<char> source = input;

                var actual = source.SplitAny(delimiters, count, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(string input, char[] delimiters, char[][] expected, int count)
            {
                ReadOnlySpan<char> source = input;

                var actual = source.SplitAny(delimiters, count, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['a', 'a']];

                var actual = source.SplitAny(['b', 'c'], 2, options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                ReadOnlySpan<char> source = "a b c a";

                char[][] expected = [['a'], [], ['a']];

                var actual = source.SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                ReadOnlySpan<char> source = "\r \n  \t";

                char[][] expected = EmptyNestedCharArray;

                var actual = source.SplitAny(['_', '!'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = EmptyNestedCharArray;

                var actual = source.SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                ReadOnlySpan<char> source = "";

                char[][] expected = EmptyNestedCharArray;

                var actual = source.SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "aabc";

                char[][] expected = [['b', 'a', 'a']];

                var actual = source.SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['a', 'a']];

                var actual = source.SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                ReadOnlySpan<char> source = " \t";

                char[][] expected = [];

                var actual = source.SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " b\taa\n";

                char[][] expected = [['b', '\t', 'a', 'a']];

                var actual = source.SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " b\taa\n";

                char[][] expected = [['a', 'a']];

                var actual = source.SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void EmptyDelimiterSpanResultsSameAsCountEqualOne(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = " ";

                var expected = source.SplitAny([], 1, options).ToSystemEnumerable();

                var actual = source.SplitAny([], options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, InvalidCountExceedingBehaviour));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], InvalidStringSplitOptions));
            }
        }
    }
}
