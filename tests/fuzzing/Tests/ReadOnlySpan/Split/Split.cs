using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class ReadOnlySpanSplitTests
    {
        public sealed class Split
        {
            [Fact]
            public void FuzzSplit()
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

                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                int integerDelimiter = integerArray[random.Next(integerArray.Length)];
                AssertOptions(integerArray, integerDelimiter);
                AssertOptions(integerArray, maxValue);

                char[] charArray = GenerateRandomString(count).ToCharArray();
                char charDelimiter = charArray[random.Next(charArray.Length)];
                const char charMissingDelimiter = '!';
                AssertOptions(charArray, charDelimiter);
                AssertOptions(charArray, charMissingDelimiter);
            }

            [Fact]
            public void FuzzSplitWithCount()
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

                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                int integerDelimiter = integerArray[random.Next(integerArray.Length)];
                int countDelimiters = integerArray.AsSpan().Count(integerDelimiter);
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertOptions(integerArray, integerDelimiter, countDelimiters, countExceedingBehaviour);
                    AssertOptions(integerArray, maxValue, countDelimiters, countExceedingBehaviour);
                }

                char[] charArray = GenerateRandomString(count).ToCharArray();
                char charDelimiter = charArray[random.Next(charArray.Length)];
                const char charMissingDelimiter = '!';
                countDelimiters = charArray.AsSpan().Count(charDelimiter);
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertOptions(charArray, charDelimiter, countDelimiters, countExceedingBehaviour);
                    AssertOptions(charArray, charMissingDelimiter, countDelimiters, countExceedingBehaviour);
                }
            }

            [Fact]
            public void FuzzSplitWithDelimiterSequence()
            {
                static void AssertOptions<T>(T[] array, T[] delimiter) where T : IEquatable<T>
                {
                    AssertMethodResults(
                        expected: Split(array, delimiter),
                        actual: array.AsReadOnlySpan().Split(delimiter).ToSystemEnumerable(),
                        source: array,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", delimiter)
                    );
                }

                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                int startIndex = random.Next(integerArray.Length - 3);
                int[] integerSequenceDelimiter = integerArray[startIndex..(startIndex + 3)];
                int[] integerSequenceMissingDelimiter = integerSequenceDelimiter.ReplaceAt(2, maxValue);
                AssertOptions(integerArray, integerSequenceDelimiter);
                AssertOptions(integerArray, integerSequenceMissingDelimiter);

                char[] charArray = GenerateRandomString(count).ToCharArray();
                startIndex = random.Next(charArray.Length - 3);
                char[] charSequenceDelimiter = charArray[startIndex..(startIndex + 3)];
                char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceAt(2, '!');
                AssertOptions(charArray, charSequenceDelimiter);
                AssertOptions(charArray, charSequenceMissingDelimiter);
            }

            [Fact]
            public void FuzzSplitWithDelimiterSequenceAndCount()
            {
                static void AssertOptions<T>(T[] array, T[] delimiter, int count, CountExceedingBehaviour countExceedingBehaviour) where T : IEquatable<T>
                {
                    AssertMethodResults(
                        expected: Split(array, delimiter, count, countExceedingBehaviour),
                        actual: array.AsReadOnlySpan().Split(delimiter, count, countExceedingBehaviour).ToSystemEnumerable(),
                        source: array,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", delimiter), ("count", count), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                int startIndex = random.Next(integerArray.Length - 3);
                int[] integerSequenceDelimiter = integerArray[startIndex..(startIndex + 3)];
                int[] integerSequenceMissingDelimiter = integerSequenceDelimiter.ReplaceAt(2, maxValue);
                int countDelimiters = integerArray.AsSpan().CountSequence(integerSequenceDelimiter);
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertOptions(integerArray, integerSequenceDelimiter, countDelimiters, countExceedingBehaviour);
                    AssertOptions(integerArray, integerSequenceMissingDelimiter, countDelimiters, countExceedingBehaviour);
                }

                char[] charArray = GenerateRandomString(count).ToCharArray();
                startIndex = random.Next(charArray.Length - 3);
                char[] charSequenceDelimiter = charArray[startIndex..(startIndex + 3)];
                char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceAt(2, '!');
                countDelimiters = charArray.AsSpan().CountSequence(charSequenceDelimiter);
                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertOptions(charArray, charSequenceDelimiter, countDelimiters, countExceedingBehaviour);
                    AssertOptions(charArray, charSequenceMissingDelimiter, countDelimiters, countExceedingBehaviour);
                }
            }
        }
    }
}
