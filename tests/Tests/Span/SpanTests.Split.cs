using static SpanExtensions.Testing.TestHelper;

namespace SpanExtensions.Testing
{
    public static class SpanSplitTests
    {
        public sealed class Split
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().Split('b').ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().Split('b').ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().Split('b', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aabab".ToCharArray().AsSpan().Split('b', 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabb".ToCharArray().AsSpan().Split('b', 0, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabb".ToCharArray().AsSpan().Split('c', 0, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('b', -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('c', -1));
            }
        }

        public sealed class SplitString
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().Split('b', StringSplitOptions.None).ToSystemEnumerable()
                );
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
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaabaa".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aab".ToCharArray().AsSpan().Split('b', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().Split('b', 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                AssertEqual(
                    [['a'], ['a']],
                    " a b a ".ToCharArray().AsSpan().Split('b', StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void EmptySpanWithRemoveEmptyEntriesOptionReturnsNothing()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().Split('_', StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    "  ".ToCharArray().AsSpan().Split('_', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
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
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabb".ToCharArray().AsSpan().Split('b', 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabb".ToCharArray().AsSpan().Split('c', 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('b', -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".ToCharArray().AsSpan().Split('c', -1, StringSplitOptions.None));
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
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [[' ', 'b', ' ', 'a', 'a']],
                    " b aa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [[' ', 'b', ' ', 'b', ' ', 'a', 'a']],
                    " b b aa".ToCharArray().AsSpan().Split('b', 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }
        }

        public sealed class SplitAny
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c']).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 0, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], 0, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], -1));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], -1));
            }
        }

        public sealed class SplitAnyString
        {
            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    "abba".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    "abca".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                AssertEqual(
                    [['a', 'a'], []],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], []],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'c']],
                    "aabac".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'b']],
                    "aacab".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
            {
                AssertEqual(
                    [['a', 'a', 'b', 'a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a', 'c', 'a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'c', 'a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a', 'b', 'a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aabaacaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a'], ['a', 'a']],
                    "aacaabaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aab".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aac".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 2, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceCharactersAssumedWhenDelimitersCollectionIsEmpty()
            {
                AssertEqual(
                    [['a'], ['b'], ['c'], ['d']],
                    "a b c d".ToCharArray().AsSpan().SplitAny([], StringSplitOptions.None).ToSystemEnumerable()
                );
            }

            [Fact]
            public void TrimEntriesOptionTrimsLastSpan()
            {
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b b a ".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a'], [], ['a']],
                    " a b c a ".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.TrimEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void EmptySpanWithRemoveEmptyEntriesOptionReturnsNothing()
            {
                AssertEqual(
                    [],
                    "".ToCharArray().AsSpan().SplitAny(['_', '!'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
            {
                AssertEqual(
                    [],
                    "  ".ToCharArray().AsSpan().SplitAny(['_', '!'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
            {
                AssertEqual(
                    [['a', 'a']],
                    "aabb".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['a', 'a']],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualZeroReturnsNothing()
            {
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
                AssertEqual(
                    [],
                    "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], 0, StringSplitOptions.None, CountExceedingBehaviour.CutLastElements).ToSystemEnumerable()
                );
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['b', 'c'], -1, StringSplitOptions.None));
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabc".ToCharArray().AsSpan().SplitAny(['d', 'e'], -1, StringSplitOptions.None));
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
            {
                AssertEqual(
                    [['b', 'a', 'a']],
                    "baa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [['b', 'c', 'a', 'a']],
                    "bcaa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }

            [Fact]
            public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
            {
                AssertEqual(
                    [[' ', 'b', ' ', 'a', 'a']],
                    " b aa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
                AssertEqual(
                    [[' ', 'b', ' ', 'c', ' ', 'a', 'a']],
                    " b c aa".ToCharArray().AsSpan().SplitAny(['b', 'c'], 1, StringSplitOptions.RemoveEmptyEntries).ToSystemEnumerable()
                );
            }
        }
    }
}