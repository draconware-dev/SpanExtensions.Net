using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SpanSplitTests
    {
        public sealed class Split
        {
            [Fact]
            public void EnumerationReturnsSpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "aba".ToCharArray().AsSpan().Split('b'))
                {
                    Assert.True(span is Span<char>);
                }

                foreach(var span in "aba".ToCharArray().AsSpan().Split('b', 10))
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
                    "".ToCharArray().AsSpan().Split('a').ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split('c').ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertEqual(
                        [],
                        "abba".ToCharArray().AsSpan().Split('a', 0, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        [],
                        "abba".ToCharArray().AsSpan().Split('c', 0, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split('a', 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split('c', 1).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().Split('b').ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "baa".ToCharArray().AsSpan().Split('b').ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().Split('b').ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().Split('b', 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aabab".ToCharArray().AsSpan().Split('b', 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aab".ToCharArray().AsSpan().Split('b', 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aab".ToCharArray().AsSpan().Split('b', 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabab".ToCharArray().AsSpan().Split('b', 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabab".ToCharArray().AsSpan().Split('b', 2).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabaa".ToCharArray().AsSpan().Split('b', 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('b', -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('c', -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split('b', 1, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split('c', 1, (CountExceedingBehaviour)255));
            }
        }
    }
}
