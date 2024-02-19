using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SplitTests
    {
        public sealed class SplitAny
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 0, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], 0, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], -1));
            }
        }
    }
}
