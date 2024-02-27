using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitSequence
        {
            [Fact]
            public void EnumerationReturnsSpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "abca".ToCharArray().AsSpan().Split(['b', 'c']))
                {
                    Assert.True(span is Span<char>);
                }

                foreach(var span in "abca".ToCharArray().AsSpan().Split(['b', 'c'], 10))
                {
                    Assert.True(span is Span<char>);
                }
#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
            }

            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                AssertEqual(
                    [[]],
                    "".ToCharArray().AsSpan().Split(['a', 'b']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split([]).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertEqual(
                        [],
                        "abba".ToCharArray().AsSpan().Split(['a', 'b'], 0, countExceedingBehaviour).ToSystemEnumerable()
                    );
                    AssertEqual(
                        [],
                        "abba".ToCharArray().AsSpan().Split(['b', 'c'], 0, countExceedingBehaviour).ToSystemEnumerable()
                    );
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split(['a', 'b'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abcbca".ToCharArray().AsSpan().Split(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "bcaa".ToCharArray().AsSpan().Split(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c']],
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b', 'c']],
                    "aabcabc".ToCharArray().AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcabc".ToCharArray().AsSpan().Split(['b', 'c'], 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcabc".ToCharArray().AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabcbc".ToCharArray().AsSpan().Split(['b', 'c'], -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabcbc".ToCharArray().AsSpan().Split(['c', 'd'], -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabcbc".ToCharArray().AsSpan().Split(['b', 'c'], 1, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabcbc".ToCharArray().AsSpan().Split(['c', 'd'], 1, (CountExceedingBehaviour)255));
            }
        }
    }
}
