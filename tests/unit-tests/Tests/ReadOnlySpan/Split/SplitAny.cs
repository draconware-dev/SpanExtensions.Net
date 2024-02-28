﻿using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class SplitAny
        {
//            [Fact]
//            public void EnumerationReturnsReadOnlySpans()
//            {
//#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
//                foreach(var span in "abaca".AsSpan().SplitAny(['b', 'c']))
//                {
//                    Assert.True(span is ReadOnlySpan<char>);
//                }

//                foreach(var span in "abaca".AsSpan().SplitAny(['b', 'c'], 10))
//                {
//                    Assert.True(span is ReadOnlySpan<char>);
//                }
//#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
//            }

            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                AssertEqual(
                    [[]],
                    "".AsSpan().SplitAny(['a', 'b']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().SplitAny(['c', 'd']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().SplitAny([]).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertEqual(
                        [],
                        "abba".AsSpan().SplitAny(['a', 'b'], 0, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        [],
                        "abba".AsSpan().SplitAny(['c', 'd'], 0, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().SplitAny(['a', 'b'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().SplitAny(['c', 'd'], 1).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "baa".AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [[], ['a', 'a']],
                    "caa".AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aab".AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aab".AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabac".AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabac".AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aacaa".AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aacaa".AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aacaabaa".AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aacaabaa".AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().SplitAny(['d', 'e'], -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().SplitAny(['d', 'e'], 1, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().SplitAny(['d', 'e'], 1, (CountExceedingBehaviour)255));
            }
        }
    }
}
