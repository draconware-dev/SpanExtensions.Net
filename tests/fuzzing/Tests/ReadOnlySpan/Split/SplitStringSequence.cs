﻿using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class ReadOnlySpanSplitTests
    {
        public static class SplitStringSequence
        {
            public static TheoryData<int, int, int> SplitWithDelimiterSequenceData(int iterations)
            {
                TheoryData<int, int, int> data = new();

                foreach(int length in new MultiplierRange(1, 1000, 10).And([0]))
                {
                    foreach(int delimiterLength in new MultiplierRange(3, length * 10, 10).And([0, 1]))
                    {
                        data.Add(iterations, length, delimiterLength);
                    }
                }

                return data;
            }

            public sealed class SplitWithDelimiterSequence
            {
                public static readonly TheoryData<int, int, int> _SplitWithDelimiterSequenceData = SplitWithDelimiterSequenceData(30000);

                [Theory]
                [MemberData(nameof(_SplitWithDelimiterSequenceData))]
                public void Fuzz(int iterations, int length, int delimiterLength)
                {
                    static void AssertOptions(string @string, char[] delimiter, StringSplitOptions options)
                    {
                        AssertMethodResults(
                            expected: @string.Split(new string(delimiter), options),
                            actual: @string.AsSpan().Split(delimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", delimiter), ("options", options)]
                        );
                    }

                    char[] randomcharDelimiterArray = GenerateRandomString(delimiterLength).ToCharArray();
                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        string @string = GenerateRandomString(length);
                        char[] charSequenceDelimiter = @string.RandomSubsequenceOrDefault(delimiterLength, randomcharDelimiterArray);
                        char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceRandomElement('ა');
                        foreach(StringSplitOptions options in stringSplitOptions)
                        {
                            AssertOptions(@string, charSequenceDelimiter, options);
                            AssertOptions(@string, charSequenceMissingDelimiter, options);
                        }
                    }
                }
            }

            public sealed class SplitWithDelimiterSequenceAndCount
            {
                public static readonly TheoryData<int, int, int> _SplitWithDelimiterSequenceData = SplitWithDelimiterSequenceData(10000);

                [Theory]
                [MemberData(nameof(_SplitWithDelimiterSequenceData))]
                public void Fuzz(int iterations, int length, int delimiterLength)
                {
                    static void AssertOptions(string @string, char[] delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                    {
                        AssertMethodResults(
                            expected: @string.Split(new string(delimiter), count, options, countExceedingBehaviour),
                            actual: @string.AsSpan().Split(delimiter, count, options, countExceedingBehaviour).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", delimiter), ("count", count), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                        );
                    }

                    char[] randomcharDelimiterArray = GenerateRandomString(delimiterLength).ToCharArray();
                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        string @string = GenerateRandomString(length);
                        char[] charSequenceDelimiter = @string.RandomSubsequenceOrDefault(delimiterLength, randomcharDelimiterArray);
                        char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceRandomElement('ა');
                        int countDelimiters = @string.CountSubsequence(charSequenceDelimiter);
                        foreach(StringSplitOptions options in stringSplitOptions)
                        {
                            foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                            {
                                AssertOptions(@string, charSequenceDelimiter, 0, options, countExceedingBehaviour);
                                AssertOptions(@string, charSequenceDelimiter, 1, options, countExceedingBehaviour);
                                if(countDelimiters - 1 > 1)
                                {
                                    AssertOptions(@string, charSequenceDelimiter, countDelimiters - 1, options, countExceedingBehaviour);
                                }

                                if(countDelimiters > 1)
                                {
                                    AssertOptions(@string, charSequenceDelimiter, countDelimiters, options, countExceedingBehaviour);
                                }

                                AssertOptions(@string, charSequenceDelimiter, countDelimiters + 2, options, countExceedingBehaviour);
                                AssertOptions(@string, charSequenceMissingDelimiter, 0, options, countExceedingBehaviour);
                                AssertOptions(@string, charSequenceMissingDelimiter, 1, options, countExceedingBehaviour);
                                AssertOptions(@string, charSequenceMissingDelimiter, 2, options, countExceedingBehaviour);
                            }
                        }
                    }
                }
            }
        }
    }
}
