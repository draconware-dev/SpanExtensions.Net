using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        public static class SplitAny
        {
            public static TheoryData<int, int, int, int, int, float> SplitAnyData(int iterations)
            {
                const int minValue = 0;
                const int maxValue = 100;

                TheoryData<int, int, int, int, int, float> data = new();

                foreach(int length in new MultiplierRange(1, 1000, 10).And([0]))
                {
                    foreach(int delimitersLength in ((IEnumerable<int>)[0, 1, 5, 25, 50]).Where(x => x <= length * 3))
                    {
                        foreach(float delimitersOccurencePart in (IEnumerable<float>)(delimitersLength > 1 ? [0f, 0.5f, 1f] : [0f, 1f]))
                        {
                            data.Add(iterations, length, minValue, maxValue, delimitersLength, delimitersOccurencePart);
                        }
                    }
                }

                return data;
            }

            public sealed class SplitAnyWithoutParameters
            {
                public static readonly TheoryData<int, int, int, int, int, float> _SplitAnyData = SplitAnyData(11000);

                [Theory]
                [MemberData(nameof(_SplitAnyData))]
                public void Fuzz(int iterations, int length, int minValue, int maxValue, int delimitersLength, float delimitersOccurencePart)
                {
                    static void AssertOptions<T>(T[] array, T[] delimiters) where T : IEquatable<T>
                    {
                        AssertMethodResults(
                            expected: SplitAny(array, delimiters),
                            actual: array.AsSpan().SplitAny(delimiters).ToSystemEnumerable(),
                            source: array,
                            method: nameof(SpanExtensions.SplitAny),
                            args: ("delimiters", delimiters)
                        );
                    }

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        int[] integerArray = GenerateRandomIntegers(length, minValue, maxValue).ToArray();
                        int[] integerDelimiters = Enumerable.Range(0, delimitersLength).Select(i =>
                            i < delimitersLength * delimitersOccurencePart ? integerArray.RandomElementOrDefault()
                            : maxValue + i
                        ).ToArray();
                        AssertOptions(integerArray, integerDelimiters);

                        char[] charArray = GenerateRandomString(length).ToCharArray();
                        char[] charDelimiters = Enumerable.Range(0, delimitersLength).Select(i =>
                            i < delimitersLength * delimitersOccurencePart ? charArray.RandomElementOrDefault()
                            : (char)('ა' + i)
                        ).ToArray();
                        AssertOptions(charArray, charDelimiters);
                    }
                }
            }

            public sealed class SplitAnyWithCount
            {
                public static readonly TheoryData<int, int, int, int, int, float> _SplitAnyData = SplitAnyData(2000);

                [Theory]
                [MemberData(nameof(_SplitAnyData))]
                public void Fuzz(int iterations, int length, int minValue, int maxValue, int delimitersLength, float delimitersOccurencePart)
                {
                    static void AssertOptions<T>(T[] array, T[] delimiters, int count, CountExceedingBehaviour countExceedingBehaviour) where T : IEquatable<T>
                    {
                        AssertMethodResults(
                            expected: SplitAny(array, delimiters, count, countExceedingBehaviour),
                            actual: array.AsSpan().SplitAny(delimiters, count, countExceedingBehaviour).ToSystemEnumerable(),
                            source: array,
                            method: nameof(SpanExtensions.SplitAny),
                            args: [("delimiters", delimiters), ("count", count), ("countExceedingBehaviour", countExceedingBehaviour)]
                        );
                    }

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        int[] integerArray = GenerateRandomIntegers(length, minValue, maxValue).ToArray();
                        int[] integerDelimiters = Enumerable.Range(0, delimitersLength).Select(i =>
                            i < delimitersLength * delimitersOccurencePart ? integerArray.RandomElementOrDefault()
                            : maxValue + i
                        ).ToArray();
                        int countDelimiters = integerArray.Count(integerDelimiters);
                        foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                        {
                            AssertOptions(integerArray, integerDelimiters, 0, countExceedingBehaviour);
                            AssertOptions(integerArray, integerDelimiters, 1, countExceedingBehaviour);
                            if(countDelimiters - 1 > 1) AssertOptions(integerArray, integerDelimiters, countDelimiters, countExceedingBehaviour);
                            if(countDelimiters > 1) AssertOptions(integerArray, integerDelimiters, countDelimiters, countExceedingBehaviour);
                            AssertOptions(integerArray, integerDelimiters, countDelimiters + 2, countExceedingBehaviour);
                        }

                        char[] charArray = GenerateRandomString(length).ToCharArray();
                        char[] charDelimiters = Enumerable.Range(0, delimitersLength).Select(i =>
                            i < delimitersLength * delimitersOccurencePart ? charArray.RandomElementOrDefault()
                            : (char)('ა' + i)
                        ).ToArray();
                        countDelimiters = charArray.Count(charDelimiters);
                        foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                        {
                            AssertOptions(charArray, charDelimiters, 0, countExceedingBehaviour);
                            AssertOptions(charArray, charDelimiters, 0, countExceedingBehaviour);
                            if(countDelimiters - 1 > 1) AssertOptions(charArray, charDelimiters, countDelimiters - 1, countExceedingBehaviour);
                            if(countDelimiters > 1) AssertOptions(charArray, charDelimiters, countDelimiters, countExceedingBehaviour);
                            AssertOptions(charArray, charDelimiters, countDelimiters + 2, countExceedingBehaviour);
                        }
                    }
                }
            }
        }
    }
}
