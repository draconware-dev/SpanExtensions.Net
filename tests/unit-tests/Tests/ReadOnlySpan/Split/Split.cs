using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class Split
        {
            [Fact]
            public void EnumerationReturnsReadOnlySpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "aba".AsSpan().Split('b'))
                {
                    Assert.True(span is ReadOnlySpan<char>);
                }

                foreach(var span in "aba".AsSpan().Split('b', 10))
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
                    "".AsSpan().Split('a').ToSystemEnumerable()
                );
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split('c').ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertEqual(
                        [],
                        "abba".AsSpan().Split('a', 0, countExceedingBehaviour).ToSystemEnumerable()
                    );
                    AssertEqual(
                        [],
                        "abba".AsSpan().Split('c', 0, countExceedingBehaviour).ToSystemEnumerable()
                    );
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split('a', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split('c', 1).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".AsSpan().Split('b').ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "baa".AsSpan().Split('b').ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".AsSpan().Split('b').ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".AsSpan().Split('b', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aabaabaa".AsSpan().Split('b', 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".AsSpan().Split('b', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aabab".AsSpan().Split('b', 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aab".AsSpan().Split('b', 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aab".AsSpan().Split('b', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabab".AsSpan().Split('b', 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabab".AsSpan().Split('b', 2).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabaa".AsSpan().Split('b', 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabaa".AsSpan().Split('b', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabaabaa".AsSpan().Split('b', 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabaabaa".AsSpan().Split('b', 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".AsSpan().Split('b', 1, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaabaa".AsSpan().Split('b', 2, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".AsSpan().Split('b', -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".AsSpan().Split('c', -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().Split('b', 1, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().Split('c', 1, (CountExceedingBehaviour)255));
            }
        }
    }
}
