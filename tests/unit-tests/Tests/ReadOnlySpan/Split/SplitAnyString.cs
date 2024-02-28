﻿using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class SplitAnyString
        {
            [Fact]
            public void EnumerationReturnsReadOnlySpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "abaca".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None))
                {
                    Assert.True(span is ReadOnlySpan<char>);
                }

                foreach(var span in "abaca".AsSpan().SplitAny(['b', 'c'], 10, StringSplitOptions.None))
                {
                    Assert.True(span is ReadOnlySpan<char>);
                }
#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
            }

            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    if(!options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        AssertEqual(
                            [[]],
                            "".AsSpan().SplitAny(['a', 'b'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertEqual(
                        ["abba".ToCharArray()],
                        "abba".AsSpan().SplitAny(['c', 'd'], options).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void WhiteSpaceCharactersAssumedWhenDelimitersSpanIsEmpty()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertEqual(
                        [['a'], ['b'], ['c'], ['d']],
                        "a b c d".AsSpan().SplitAny([], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                    {
                        AssertEqual(
                            [],
                            "abba".AsSpan().SplitAny(['a', 'b'], 0, options, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [],
                            "abba".AsSpan().SplitAny(['c', 'd'], 0, options, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().SplitAny(['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().SplitAny(['c', 'd'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    if(options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        AssertEqual(
                            [['a'], ['a']],
                            "abba".AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a'], ['a']],
                            "abca".AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtStartEndResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "baa".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [[], ['a', 'a']],
                    "caa".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheStartWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    if(options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        AssertEqual(
                            [['a', 'a']],
                            "baa".AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a', 'a']],
                            "caa".AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    if(options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        AssertEqual(
                            [['a', 'a']],
                            "aab".AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a', 'a']],
                            "aac".AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabaabaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabaabaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aab".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aab".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabab".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabab".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    if(options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        AssertEqual(
                            [['a', 'a']],
                            "aabb".AsSpan().SplitAny(['b', 'c'], 2, options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a', 'a']],
                            "aabc".AsSpan().SplitAny(['b', 'c'], 2, options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b b a ".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b c a ".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    " \t".AsSpan().SplitAny(['_', '!'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "aabc".AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                AssertEqual(
                    [],
                    "".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'a', 'a']],
                    "baa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['b', 'c', 'a', 'a']],
                    "bcaa".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    "baa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "bcaa".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                AssertEqual(
                    [],
                    " \t".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['b', '\t', 'a', 'a']],
                    " b\taa\n".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['b', '\t', 'c', '\n', 'a', 'a']],
                    " b\tc\naa\r".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    " b\taa\n".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    " b\tc\naa\r".AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void EmptyDelimiterSpanResultsSameAsCountEqualOne()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertEqual(
                        "".AsSpan().SplitAny([], 1, options).ToSystemEnumerable(maxCount: 100),
                        "".AsSpan().SplitAny([], options).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        " ".AsSpan().SplitAny([], 1, options).ToSystemEnumerable(maxCount: 100),
                        " ".AsSpan().SplitAny([], options).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        " aabb ".AsSpan().SplitAny([], 1, options).ToSystemEnumerable(maxCount: 100),
                        " aabb ".AsSpan().SplitAny([], options).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().SplitAny(['d', 'e'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().SplitAny(['d', 'e'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().SplitAny(['b', 'c'], (StringSplitOptions)255));
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().SplitAny(['d', 'e'], (StringSplitOptions)255));
            }
        }
    }
}
