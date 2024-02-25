using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitStringSequence
        {
            [Fact]
            public void EnumerationReturnsSpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "aba".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.None))
                {
                    Assert.True(span is Span<char>);
                }

                foreach(var span in "aba".ToCharArray().AsSpan().Split(['b', 'c'], 10, StringSplitOptions.None))
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
                            "".ToCharArray().AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
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
                        "abba".ToCharArray().AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                    );
                }
            }

            [Fact]
            public void EmptyDelimiterSpanResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split([], StringSplitOptions.None).ToSystemEnumerable()
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
                            "abba".ToCharArray().AsSpan().Split(['a', 'b'], 0, options, countExceedingBehaviour).ToSystemEnumerable()
                        );
                        AssertEqual(
                            [],
                            "abba".ToCharArray().AsSpan().Split(['b', 'c'], 0, options, countExceedingBehaviour).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split(['a', 'b'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abcbca".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
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
                            "abcbca".ToCharArray().AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "bcaa".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
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
                            "bcaa".ToCharArray().AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
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
                            "aabc".ToCharArray().AsSpan().Split(['b', 'c'], options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'c', 'a', 'a']],
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'c']],
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b', 'c']],
                    "aabcabc".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabcabc".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabcabc".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabcaabcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
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
                            "aabcbc".ToCharArray().AsSpan().Split(['b', 'c'], 2, options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void TrimEntriesOptionTrimsEverySpan()
            {
                AssertEqual(
                    [['a'], ['a']],
                    " a\tbc\na\r".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    " \t".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabcbc".ToCharArray().AsSpan().Split(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'c', 'a', 'a']],
                    "bcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'c', 'b', 'c', 'a', 'a']],
                    "bcbcaa".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    "bcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "bcbcaa".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                AssertEqual(
                    [],
                    " \t".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'c', '\t', 'a', 'a']],
                    " bc\taa\n".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'c', '\t', 'b', 'c', '\n', 'a', 'a']],
                    " bc\tbc\naa\r".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    " bc\taa\n".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    " bc\tbc\naa\r".ToCharArray().AsSpan().Split(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().Split(['b', 'c'], -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split(['b', 'c'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split(['b', 'c'], 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabc".ToCharArray().AsSpan().Split(['b', 'c'], (StringSplitOptions)255));
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split(['b', 'c'], (StringSplitOptions)255));
            }
        }
    }
}
