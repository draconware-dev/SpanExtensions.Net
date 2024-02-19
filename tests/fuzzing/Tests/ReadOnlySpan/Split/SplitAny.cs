using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class SplitAny
        {
            [Fact]
            public void FuzzSplitAny()
            {
                static void AssertOptions<T>(T[] array, T[] delimiters) where T : IEquatable<T>
                {
                    AssertMethodResults(
                        expected: SplitAny(array, delimiters),
                        actual: array.AsReadOnlySpan().SplitAny(delimiters).ToSystemEnumerable(),
                        source: array,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: ("delimiters", delimiters)
                    );
                }

                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                int[] integerDelimiters = Enumerable.Range(0, 5).Select(_ => integerArray[random.Next(integerArray.Length)]).ToArray();
                int[] integerMissingDelimiters = Enumerable.Range(0, 5).Select(i => maxValue + i).ToArray();
                AssertOptions(integerArray, integerDelimiters);
                AssertOptions(integerArray, integerMissingDelimiters);

                char[] charArray = GenerateRandomString(length).ToCharArray();
                char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => charArray[random.Next(charArray.Length)]).ToArray();
                char[] charMissingDelimiters = Enumerable.Range(0, 5).Select(i => (char)('ა' + i)).ToArray();
                AssertOptions(charArray, charDelimiters);
                AssertOptions(charArray, charMissingDelimiters);
            }

            [Fact]
            public void FuzzSplitAnyWithCount()
            {
                static void AssertOptions<T>(T[] array, T[] delimiters, int count, CountExceedingBehaviour countExceedingBehaviour) where T : IEquatable<T>
                {
                    AssertMethodResults(
                        expected: SplitAny(array, delimiters, count, countExceedingBehaviour),
                        actual: array.AsReadOnlySpan().SplitAny(delimiters, count, countExceedingBehaviour).ToSystemEnumerable(),
                        source: array,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: [("delimiters", delimiters), ("count", count), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                int[] integerDelimiters = Enumerable.Range(0, 5).Select(_ => integerArray[random.Next(integerArray.Length)]).ToArray();
                int[] integerMissingDelimiters = Enumerable.Range(0, 5).Select(i => maxValue + i).ToArray();
                int countDelimiters = integerArray.AsSpan().Count(integerDelimiters);
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertOptions(integerArray, integerDelimiters, countDelimiters, countExceedingBehaviour);
                    AssertOptions(integerArray, integerMissingDelimiters, countDelimiters, countExceedingBehaviour);
                }

                char[] charArray = GenerateRandomString(length).ToCharArray();
                char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => charArray[random.Next(charArray.Length)]).ToArray();
                char[] cahrMissingDelimiters = Enumerable.Range(0, 5).Select(i => (char)('ა' + i)).ToArray();
                countDelimiters = charArray.AsSpan().Count(charDelimiters);
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertOptions(charArray, charDelimiters, countDelimiters, countExceedingBehaviour);
                    AssertOptions(charArray, cahrMissingDelimiters, countDelimiters, countExceedingBehaviour);
                }
            }
        }
    }
}
