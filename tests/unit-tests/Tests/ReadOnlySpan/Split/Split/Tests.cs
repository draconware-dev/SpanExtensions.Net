﻿using static SpanExtensions.Tests.UnitTests.TestHelper;

namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed partial class Split
        {
            //            [Fact]
            //            public void EnumerationReturnsReadOnlySpans()
            //            {
            //#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
            //                foreach(var span in "aba".AsSpan().Split('b'))
            //                {
            //                    Assert.True(span is ReadOnlySpan<char>);
            //                }

            //                foreach(var span in "aba".AsSpan().Split('b', 10))
            //                {
            //                    Assert.True(span is ReadOnlySpan<char>);
            //                }
            //#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
            //            }

            [Fact]
            public void EmptySourceResultInEmptySpan()
            {
                ReadOnlySpan<char> emptySpan = "";

                var expected = EmptyNestedCharArray;

                var actual = emptySpan.Split('a').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NoDelimiterOccurenceResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.Split('c').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void CountEqualZeroResultsInNothing()
            {
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    ReadOnlySpan<char> source = ABBAArray;

                    var expected = EmptyNestedCharArray;

                    var actual = source.Split('a', 0, countExceedingBehaviour).ToSystemEnumerable();

                    AssertEqual(expected, actual);
                }
            }

            [Fact]
            public void CountEqualOneResultsInNoChange()
            {
                ReadOnlySpan<char> source = ABBAArray;

                var expected = NestedABBAArray;

                var actual = source.Split('a', 1).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void ConsecutiveDelimitersResultInEmptySpan()
            {
                ReadOnlySpan<char> source = ABBAArray;

                char[][] expected = [['a'], [], ['a']];

                var actual = source.Split('b').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheStartResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "baa";

                char[][] expected = [[], ['a', 'a']];

                var actual = source.Split('b').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void DelimiterAtTheEndResultInEmptySpan()
            {
                ReadOnlySpan<char> source = "aab";

                char[][] expected = [['a', 'a'], []];

                var actual = source.Split('b').ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter_Data))]
            public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = source.Split(delimiter, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter_Data))]
            public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = source.Split(delimiter, count).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Theory]
            [MemberData(nameof(CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut_Data))]
            public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut(char[][] expected, string sourceString, int count, char delimiter)
            {
                ReadOnlySpan<char> source = sourceString;

                var actual = source.Split(delimiter, count, CountExceedingBehaviour.CutRemainingElements).ToSystemEnumerable();

                AssertEqual(expected, actual);
            }

            [Fact]
            public void NegativeCountThrowsArgumentOutOfRangeException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => "aabb".AsSpan().Split('b', -1));
            }

            [Fact]
            public void UndefinedCountExceedingBehaviourOptionThrowsArgumentException()
            {
                Assert.Throws<ArgumentException>(() => "aabb".AsSpan().Split('b', 1, InvalidCountExceedingBehaviour));
            }
        }
    }
}