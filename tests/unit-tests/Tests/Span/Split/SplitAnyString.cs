using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitAnyString
        {
            [Fact]
            public void EnumerationReturnsSpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "abaca".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None))
                {
                    Assert.True(span is Span<char>);
                }

                foreach(var span in "abaca".ToCharArray().AsSpan().SplitAny(['b', 'c'], 10, StringSplitOptions.None))
                {
                    Assert.True(span is Span<char>);
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
                            "".ToCharArray().AsSpan().SplitAny(['a', 'b'], options).ToSystemEnumerable(maxCount: 100)
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
                        "abba".ToCharArray().AsSpan().SplitAny(['c', 'd'], options).ToSystemEnumerable(maxCount: 100)
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
                        "a b c d".ToCharArray().AsSpan().SplitAny([], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
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
                            "abba".ToCharArray().AsSpan().SplitAny(['a', 'b'], 0, options, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [],
                            "abba".ToCharArray().AsSpan().SplitAny(['c', 'd'], 0, options, countExceedingBehaviour).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().SplitAny(['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().SplitAny(['c', 'd'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
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
                            "abba".ToCharArray().AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a'], ['a']],
                            "abca".ToCharArray().AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtStartEndResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "baa".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [[], ['a', 'a']],
                    "caa".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
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
                            "baa".ToCharArray().AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a', 'a']],
                            "caa".ToCharArray().AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
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
                            "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a', 'a']],
                            "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    "aabab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(maxCount: 100),
                    "aabab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable(maxCount: 100)
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
                            "aabb".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, options).ToSystemEnumerable(maxCount: 100)
                        );
                        AssertEqual(
                            [['a', 'a']],
                            "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, options).ToSystemEnumerable(maxCount: 100)
                        );
                    }
                }
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b b a ".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b c a ".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    " \t".ToCharArray().AsSpan().SplitAny(['_', '!'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'a', 'a']],
                    "baa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['b', 'c', 'a', 'a']],
                    "bcaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    "baa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    "bcaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                AssertEqual(
                    [],
                    " \t".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['b', '\t', 'a', 'a']],
                    " b\taa\n".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['b', '\t', 'c', '\n', 'a', 'a']],
                    " b\tc\naa\r".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    " b\taa\n".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
                AssertEqual(
                    [['a', 'a']],
                    " b\tc\naa\r".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable(maxCount: 100)
                );
            }

            [Fact]
            public void EmptyDelimiterSpanResultsSameAsCountEqualOne()
            {
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertEqual(
                        "".ToCharArray().AsSpan().SplitAny([], 1, options).ToSystemEnumerable(maxCount: 100),
                        "".ToCharArray().AsSpan().SplitAny([], options).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        " ".ToCharArray().AsSpan().SplitAny([], 1, options).ToSystemEnumerable(maxCount: 100),
                        " ".ToCharArray().AsSpan().SplitAny([], options).ToSystemEnumerable(maxCount: 100)
                    );
                    AssertEqual(
                        " aabb ".ToCharArray().AsSpan().SplitAny([], 1, options).ToSystemEnumerable(maxCount: 100),
                        " aabb ".ToCharArray().AsSpan().SplitAny([], options).ToSystemEnumerable(maxCount: 100)
                    );
                }
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], (StringSplitOptions)255));
                Assert.Throws<ArgumentException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], (StringSplitOptions)255));
            }
        }
    }
}
