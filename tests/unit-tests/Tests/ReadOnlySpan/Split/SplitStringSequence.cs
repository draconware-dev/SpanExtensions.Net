using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class SplitStringSequence
        {
            [Fact]
            public void EnumerationReturnsReadOnlySpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "aba".AsSpan().Split(['b', 'c'], StringSplitOptions.None))
                {
                    Assert.True(span is ReadOnlySpan<char>);
                }

                foreach(var span in "aba".AsSpan().Split(['b', 'c'], 10, StringSplitOptions.None))
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
                            "".AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
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
                        "abba".AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                    );
                }
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split([], StringSplitOptions.None).ToSystemEnumerable()
                );
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
                            "abba".AsSpan().Split(['a', 'b'], 0, options, countExceedingBehaviour).ToSystemEnumerable()
                        );
                        AssertEqual(
                            [],
                            "abba".AsSpan().Split(['b', 'c'], 0, options, countExceedingBehaviour).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split(['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abcbca".AsSpan().Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
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
                            "abcbca".AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "bcaa".AsSpan().Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
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
                            "bcaa".AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aabc".AsSpan().Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
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
                            "aabc".AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaa".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c']],
                    "aabc".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b', 'c']],
                    "aabcabc".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabcaa".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcaa".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabc".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabc".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcabc".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcabc".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabcaa".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabcaabcaa".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
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
                            "aabcbc".AsSpan().Split(['b', 'c'], 2, options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void TrimEntriesOptionTrimsEverySpan()
            {
                AssertEqual(
                    [['a'], ['a']],
                    " a\tbc\na\r".AsSpan().Split(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    " \t".AsSpan().Split(['b', 'c'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabcbc".AsSpan().Split(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                AssertEqual(
                    [],
                    "".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'c', 'a', 'a']],
                    "bcaa".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'c', 'b', 'c', 'a', 'a']],
                    "bcbcaa".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    "bcaa".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "bcbcaa".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                AssertEqual(
                    [],
                    " \t".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'c', '\t', 'a', 'a']],
                    " bc\taa\n".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'c', '\t', 'b', 'c', '\n', 'a', 'a']],
                    " bc\tbc\naa\r".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    " bc\taa\n".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    " bc\tbc\naa\r".AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".AsSpan().Split(['b', 'c'], -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".AsSpan().Split(['b', 'c'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".AsSpan().Split(['b', 'c'], (StringSplitOptions)255));
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().Split(['b', 'c'], (StringSplitOptions)255));
            }
        }
    }
}
