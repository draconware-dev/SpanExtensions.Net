using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class SplitSequence
        {
            [Fact]
            public void EnumerationReturnsReadOnlySpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "abca".AsSpan().Split(['b', 'c']))
                {
                    Assert.True(span is ReadOnlySpan<char>);
                }

                foreach(var span in "abca".AsSpan().Split(['b', 'c'], 10))
                {
                    Assert.True(span is ReadOnlySpan<char>);
                }
#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
            }

            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                AssertEqual(
                    [[]],
                    "".AsSpan().Split(['a', 'b']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split([]).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertEqual(
                        [],
                        "abba".AsSpan().Split(['a', 'b'], 0, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        [],
                        "abba".AsSpan().Split(['b', 'c'], 0, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split(['a', 'b'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abcbca".AsSpan().Split(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "bcaa".AsSpan().Split(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aabc".AsSpan().Split(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaa".AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c']],
                    "aabc".AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b', 'c']],
                    "aabcabc".AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabc".AsSpan().Split(['b', 'c'], 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabc".AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabcabc".AsSpan().Split(['b', 'c'], 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabcabc".AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabcaa".AsSpan().Split(['b', 'c'], 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabcaa".AsSpan().Split(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabcaa".AsSpan().Split(['b', 'c'], 1, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabcbc".AsSpan().Split(['b', 'c'], -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabcbc".AsSpan().Split(['c', 'd'], -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabcbc".AsSpan().Split(['b', 'c'], 1, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabcbc".AsSpan().Split(['c', 'd'], 1, (CountExceedingBehaviour)255));
            }
        }
    }
}
