using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SplitTests
    {
        public sealed class SplitAnyString
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceCharactersAssumedWhenDelimitersCollectionIsEmpty()
            {
                AssertEqual(
                    [['a'], ['b'], ['c'], ['d']],
                    "a b c d".ToCharArray().AsSpan().SplitAny([], StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b b a ".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b c a ".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void EmptySpanWithRemoveEmptyEntriesOptionReturnsNothing()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().SplitAny(['_', '!'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    "  ".ToCharArray().AsSpan().SplitAny(['_', '!'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'a', 'a']],
                    "baa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'c', 'a', 'a']],
                    "bcaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [[' ', 'b', ' ', 'a', 'a']],
                    " b aa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [[' ', 'b', ' ', 'c', ' ', 'a', 'a']],
                    " b c aa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }
        }
    }
}
