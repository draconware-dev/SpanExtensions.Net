using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitString
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().Split('b', StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().Split('b', StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aabab".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aab".ToCharArray().AsSpan().Split('b', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                AssertEqual(
                    [['a'], ['a']],
                    " a b a ".ToCharArray().AsSpan().Split('b', StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void EmptySpanWithRemoveEmptyEntriesOptionReturnsNothing()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().Split('_', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    "  ".ToCharArray().AsSpan().Split('_', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().Split('b', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabb".ToCharArray().AsSpan().Split('b', 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabb".ToCharArray().AsSpan().Split('c', 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('b', -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('c', -1, StringSplitOptions.None));
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'a', 'a']],
                    "baa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'b', 'a', 'a']],
                    "bbaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [[' ', 'b', ' ', 'a', 'a']],
                    " b aa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [[' ', 'b', ' ', 'b', ' ', 'a', 'a']],
                    " b b aa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }
        }
    }
}
