using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class ReadOnlySpanSplitTests
    {
        public static class Split
        {
            public static TheoryData<int, int, int, int> SplitData(int iterations)
            {
                const int minValue = 0;
                const int maxValue = 100;

                TheoryData<int, int, int, int> data = new();

                foreach(int length in new MultiplierRange(1, 1000, 10).And([0]))
                {
                    data.Add(iterations, length, minValue, maxValue);
                }

                return data;
            }

            public sealed class SplitWithoutParameters
            {
                public static readonly TheoryData<int, int, int, int> _SplitData = SplitData(250000);

                [Theory]
                [MemberData(nameof(_SplitData))]
                public void Fuzz(int iterations, int length, int minValue, int maxValue)
                {
                    static void AssertOptions<T>(T[] array, T delimiter) where T : IEquatable<T>
                    {
                        AssertMethodResults(
                            expected: Split(array, delimiter),
                            actual: array.AsReadOnlySpan().Split(delimiter).ToSystemEnumerable(),
                            source: array,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: ("delimiter", delimiter)
                        );
                    }

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        int[] integerArray = GenerateRandomIntegers(length, minValue, maxValue).ToArray();
                        int integerDelimiter = integerArray.RandomElementOrDefault(0);
                        AssertOptions(integerArray, integerDelimiter);
                        AssertOptions(integerArray, maxValue);

                        char[] charArray = GenerateRandomString(length).ToCharArray();
                        char charDelimiter = charArray.RandomElementOrDefault('0');
                        const char charMissingDelimiter = 'ა';
                        AssertOptions(charArray, charDelimiter);
                        AssertOptions(charArray, charMissingDelimiter);
                    }
                }
            }

            public sealed class SplitWithCount
            {
                public static readonly TheoryData<int, int, int, int> _SplitData = SplitData(25000);

                [Theory]
                [MemberData(nameof(_SplitData))]
                public void Fuzz(int iterations, int length, int minValue, int maxValue)
                {
                    static void AssertOptions<T>(T[] array, T delimiter, int count, CountExceedingBehaviour countExceedingBehaviour) where T : IEquatable<T>
                    {
                        AssertMethodResults(
                            expected: Split(array, delimiter, count, countExceedingBehaviour),
                            actual: array.AsReadOnlySpan().Split(delimiter, count, countExceedingBehaviour).ToSystemEnumerable(),
                            source: array,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", delimiter), ("count", count), ("countExceedingBehaviour", countExceedingBehaviour)]
                        );
                    }

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        int[] integerArray = GenerateRandomIntegers(length, minValue, maxValue).ToArray();
                        int integerDelimiter = integerArray.RandomElementOrDefault(0);
                        int countDelimiters = integerArray.Count(integerDelimiter);
                        foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                        {
                            AssertOptions(integerArray, integerDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(integerArray, integerDelimiter, 1, countExceedingBehaviour);
                            if(countDelimiters - 1 > 1) AssertOptions(integerArray, integerDelimiter, countDelimiters - 1, countExceedingBehaviour);
                            if(countDelimiters > 1) AssertOptions(integerArray, integerDelimiter, countDelimiters, countExceedingBehaviour);
                            AssertOptions(integerArray, integerDelimiter, countDelimiters + 2, countExceedingBehaviour);
                            AssertOptions(integerArray, maxValue, 0, countExceedingBehaviour);
                            AssertOptions(integerArray, maxValue, 1, countExceedingBehaviour);
                            AssertOptions(integerArray, maxValue, 2, countExceedingBehaviour);
                        }

                        char[] charArray = GenerateRandomString(length).ToCharArray();
                        char charDelimiter = charArray.RandomElementOrDefault('0');
                        const char charMissingDelimiter = 'ა';
                        countDelimiters = charArray.Count(charDelimiter);
                        foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                        {
                            AssertOptions(charArray, charDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(charArray, charDelimiter, 1, countExceedingBehaviour);
                            if(countDelimiters - 1 > 1) AssertOptions(charArray, charDelimiter, countDelimiters - 1, countExceedingBehaviour);
                            if(countDelimiters > 1) AssertOptions(charArray, charDelimiter, countDelimiters, countExceedingBehaviour);
                            AssertOptions(charArray, charDelimiter, countDelimiters + 2, countExceedingBehaviour);
                            AssertOptions(charArray, charMissingDelimiter, 0, countExceedingBehaviour);
                            AssertOptions(charArray, charMissingDelimiter, 1, countExceedingBehaviour);
                            AssertOptions(charArray, charMissingDelimiter, 2, countExceedingBehaviour);
                        }
                    }
                }
            }
        }
    }
}
