using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class Split_StringSplitOptions
        {
            [Theory]
            [MemberData(nameof(StringSplitOptionsWithoutRemoveEmptyEntries))]
            public void EmptySourceResultInEmptySpanUnless_StringSplitOptions_RemoveEmptyEntries_IsSet(StringSplitOptions options)
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = ReadOnlySpanExtensions.Split(emptySpan, 'a', options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptions_Data))]
            public void NoDelimiterOccurenceResultsInNoChange(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, 'c', options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualZeroResultsInNothing_Data))]
            public void CountEqualZeroResultsInNothing(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour, char delimiter)
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, 0, options, countExceedingBehaviour).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = ReadOnlySpanExtensions.Split(source, 'a', 1, StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "abba";

                char[][] expected = [['a'], [], ['a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "abba";

                char[][] expected = [['a'], ['a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [[], ['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheStartWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', StringSplitOptions.None).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count, StringSplitOptions.None).ToSystemEnumerable();

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

                var actual = ReadOnlySpanExtensions.Split(source, delimiter, count, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(StringSplitOptionsWithRemoveEmptyEntries))]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter(StringSplitOptions options)
            {
                ReadOnlySpan<char> source = "aabb";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 2, options).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void TrimEntriesOptionTrimsEverySpan()
            {
                ReadOnlySpan<char> source = " a\tb\na\r";

                char[][] expected = [['a'], ['a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                ReadOnlySpan<char> source = " \t";

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.Split(source, '_', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                ReadOnlySpan<char> source = "aabb";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                ReadOnlySpan<char> source = "";

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['b', 'a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                ReadOnlySpan<char> source = " \t";

                char[][] expected = [];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " b\taa\n";

                char[][] expected = [['b', '\t', 'a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                ReadOnlySpan<char> source = " b\taa\n";

                char[][] expected = [['a', 'a']];

                var actual = ReadOnlySpanExtensions.Split(source, 'b', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => ReadOnlySpanExtensions.Split("aabb".AsSpan(), 'b', -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.Split("aabb".AsSpan(), 'b', 1, StringSplitOptions.None, InvalidCountExceedingBehaviour));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => ReadOnlySpanExtensions.Split("aabb".AsSpan(), 'b', InvalidStringSplitOptions));
            }
        }
    }
}
