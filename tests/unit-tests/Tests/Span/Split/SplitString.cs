using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitString
        {
            [Fact]
            public void EnumerationReturnsSpans()
            {
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
                foreach(var span in "aba".ToCharArray().AsSpan().Split('b', StringSplitOptions.None))
                {
                    Assert.True(span is Span<char>);
                }

                foreach(var span in "aba".ToCharArray().AsSpan().Split('b', 10, StringSplitOptions.None))
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
                            "".ToCharArray().AsSpan().Split('a', options).ToSystemEnumerable()
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
                        "abba".ToCharArray().AsSpan().Split('c', options).ToSystemEnumerable()
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
                            "abba".ToCharArray().AsSpan().Split('a', 0, options, countExceedingBehaviour).ToSystemEnumerable()
                        );
                        AssertEqual(
                            [],
                            "abba".ToCharArray().AsSpan().Split('c', 0, options, countExceedingBehaviour).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split('a', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    ["abba".ToCharArray()],
                    "abba".ToCharArray().AsSpan().Split('c', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().Split('b', StringSplitOptions.None).ToSystemEnumerable()
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
                            "abba".ToCharArray().AsSpan().Split('b', options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                AssertEqual(
                    [[], ['a', 'a']],
                    "baa".ToCharArray().AsSpan().Split('b', StringSplitOptions.None).ToSystemEnumerable()
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
                            "baa".ToCharArray().AsSpan().Split('b', options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().Split('b', StringSplitOptions.None).ToSystemEnumerable()
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
                            "aab".ToCharArray().AsSpan().Split('b', options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aabab".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DefaultCountExceedingBehaviourOptionIsAppendRemainingElements()
            {
                AssertEqual(
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aab".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aab".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    "aabab".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None, CountExceedingBehaviour.AppendRemainingElements).ToSystemEnumerable(),
                    "aabab".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable()
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
                            "aabb".ToCharArray().AsSpan().Split('b', 2, options).ToSystemEnumerable()
                        );
                    }
                }
            }

            [Fact]
            public void TrimEntriesOptionTrimsEverySpan()
            {
                AssertEqual(
                    [['a'], ['a']],
                    " a\tb\na\r".ToCharArray().AsSpan().Split('b', StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    " \t".ToCharArray().AsSpan().Split('_', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().Split('b', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionResultsInNothingIfSourceEmpty()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'a', 'a']],
                    "baa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'b', 'a', 'a']],
                    "bbaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesOptionRecursivelyRemovesEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    "baa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "bbaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsResultsInNothingIfSourceWhiteSpace()
            {
                AssertEqual(
                    [],
                    " \t".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['b', '\t', 'a', 'a']],
                    " b\taa\n".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', '\t', 'b', '\n', 'a', 'a']],
                    " b\tb\naa\r".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountGreaterThanOneWithRemoveEmptyEntriesAndTrimEntriesOptionsRecursivelyRemovesWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [['a', 'a']],
                    " b\taa\n".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    " b\tb\naa\r".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('b', -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('c', -1, StringSplitOptions.None));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split('c', 1, StringSplitOptions.None, (CountExceedingBehaviour)255));
            }

            [Fact]
            public void UndefinedStringSplitOptionsThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split('b', (StringSplitOptions)255));
                Assert.Throws<ArgumentException>(() => "aabb".ToCharArray().AsSpan().Split('c', (StringSplitOptions)255));
            }
        }
    }
}
