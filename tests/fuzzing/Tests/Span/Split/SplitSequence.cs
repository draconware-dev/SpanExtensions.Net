using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        public static class SplitSequence
        {
            public static TheoryData<int, int, int, int, int> SplitWithDelimiterSequenceData(int iterations)
            {
                const int minValue = 0;
                const int maxValue = 100;

                TheoryData<int, int, int, int, int> data = new();

                foreach(int length in new MultiplierRange(1, 1000, 10).And([0]))
                {
                    foreach(int delimiterLength in new MultiplierRange(3, length * 10, 10).And([0, 1]))
                    {
                        data.Add(iterations, length, minValue, maxValue, delimiterLength);
                    }
                }

                return data;
            }

            public sealed class SplitWithDelimiterSequence
            {
                public static readonly TheoryData<int, int, int, int, int> _SplitWithDelimiterSequenceData = SplitWithDelimiterSequenceData(20000);

                [Theory]
                [MemberData(nameof(_SplitWithDelimiterSequenceData))]
                public void Fuzz(int iterations, int length, int minValue, int maxValue, int delimiterLength)
                {
                    static void AssertOptions<T>(T[] array, T[] delimiter) where T : IEquatable<T>
                    {
                        AssertMethodResults(
                            expected: Split(array, delimiter),
                            actual: array.AsSpan().Split(delimiter).ToSystemEnumerable(),
                            source: array,
                            method: nameof(SpanExtensions.Split),
                            args: ("delimiter", delimiter)
                        );
                    }

                    int[] randomIntDelimiterArray = GenerateRandomIntegers(delimiterLength, minValue, maxValue).ToArray();
                    char[] randomcharDelimiterArray = GenerateRandomString(delimiterLength).ToCharArray();
                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        int[] integerArray = GenerateRandomIntegers(length, minValue, maxValue).ToArray();
                        int[] integerSequenceDelimiter = integerArray.RandomSubsequenceOrDefault(delimiterLength, randomIntDelimiterArray);
                        int[] integerSequenceMissingDelimiter = integerSequenceDelimiter.ReplaceRandomElement(maxValue);
                        AssertOptions(integerArray, integerSequenceDelimiter);
                        AssertOptions(integerArray, integerSequenceMissingDelimiter);

                        char[] charArray = GenerateRandomString(length).ToCharArray();
                        char[] charSequenceDelimiter = charArray.RandomSubsequenceOrDefault(delimiterLength, randomcharDelimiterArray);
                        char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceRandomElement('ა');
                        AssertOptions(charArray, charSequenceDelimiter);
                        AssertOptions(charArray, charSequenceMissingDelimiter);
                    }
                }
            }

            public sealed class SplitWithDelimiterSequenceAndCount
            {
                public static readonly TheoryData<int, int, int, int, int> _SplitWithDelimiterSequenceData = SplitWithDelimiterSequenceData(5000);

                [Theory]
                [MemberData(nameof(_SplitWithDelimiterSequenceData))]
                public void Fuzz(int iterations, int length, int minValue, int maxValue, int delimiterLength)
                {
                    static void AssertOptions<T>(T[] array, T[] delimiter, int count, CountExceedingBehaviour countExceedingBehaviour) where T : IEquatable<T>
                    {
                        AssertMethodResults(
                            expected: Split(array, delimiter, count, countExceedingBehaviour),
                            actual: array.AsSpan().Split(delimiter, count, countExceedingBehaviour).ToSystemEnumerable(),
                            source: array,
                            method: nameof(SpanExtensions.Split),
                            args: [("delimiter", delimiter), ("count", count), ("countExceedingBehaviour", countExceedingBehaviour)]
                        );
                    }

                    int[] randomIntDelimiterArray = GenerateRandomIntegers(delimiterLength, minValue, maxValue).ToArray();
                    char[] randomcharDelimiterArray = GenerateRandomString(delimiterLength).ToCharArray();
                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        int[] integerArray = GenerateRandomIntegers(length, minValue, maxValue).ToArray();
                        int[] integerSequenceDelimiter = integerArray.RandomSubsequenceOrDefault(delimiterLength, randomIntDelimiterArray);
                        int[] integerSequenceMissingDelimiter = integerSequenceDelimiter.ReplaceRandomElement(maxValue);
                        int countDelimiters = integerArray.CountSubsequence(integerSequenceDelimiter);
                        foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                        {
                            AssertOptions(integerArray, integerSequenceDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(integerArray, integerSequenceDelimiter, 1, countExceedingBehaviour);
                            if(countDelimiters - 1 > 1) AssertOptions(integerArray, integerSequenceDelimiter, countDelimiters - 1, countExceedingBehaviour);
                            if(countDelimiters > 1) AssertOptions(integerArray, integerSequenceDelimiter, countDelimiters, countExceedingBehaviour);
                            AssertOptions(integerArray, integerSequenceDelimiter, countDelimiters + 2, countExceedingBehaviour);
                            AssertOptions(integerArray, integerSequenceMissingDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(integerArray, integerSequenceMissingDelimiter, 1, countExceedingBehaviour);
                            AssertOptions(integerArray, integerSequenceMissingDelimiter, 2, countExceedingBehaviour);
                        }

                        char[] charArray = GenerateRandomString(length).ToCharArray();
                        char[] charSequenceDelimiter = charArray.RandomSubsequenceOrDefault(delimiterLength, randomcharDelimiterArray);
                        char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceRandomElement('ა');
                        countDelimiters = charArray.CountSubsequence(charSequenceDelimiter);
                        foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                        {
                            AssertOptions(charArray, charSequenceDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(charArray, charSequenceDelimiter, 1, countExceedingBehaviour);
                            if(countDelimiters - 1 > 1) AssertOptions(charArray, charSequenceDelimiter, countDelimiters - 1, countExceedingBehaviour);
                            if(countDelimiters > 1) AssertOptions(charArray, charSequenceDelimiter, countDelimiters, countExceedingBehaviour);
                            AssertOptions(charArray, charSequenceDelimiter, countDelimiters + 2, countExceedingBehaviour);
                            AssertOptions(charArray, charSequenceMissingDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(charArray, charSequenceMissingDelimiter, 1, countExceedingBehaviour);
                            AssertOptions(charArray, charSequenceMissingDelimiter, 2, countExceedingBehaviour);
                        }
                    }
                }
            }
        }
    }
}
