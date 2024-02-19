using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static class SpanSplitTests
    {
        const int count = 250;
        const int minValue = 0;
        const int maxValue = 100;
        const int length = 100;
        static readonly IEnumerable<StringSplitOptions> stringSplitOptions = GetAllStringSplitOptions();
        static readonly CountExceedingBehaviour[] countExceedingBehaviours = (CountExceedingBehaviour[])Enum.GetValues(typeof(CountExceedingBehaviour));

        public sealed class Split()
        {
            [Fact]
            public void FuzzSplit()
            {
                static void AssertOptions<T>(T[] array, T delimiter) where T : IEquatable<T>
                {
                    AssertMethodResults(
                        expected: Split(array, delimiter),
                        actual: array.AsSpan().Split(delimiter).ToSystemEnumerable(),
                        source: array,
                        method: nameof(SpanExtensions.Split),
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
                        actual: array.AsSpan().Split(delimiter, count, countExceedingBehaviour).ToSystemEnumerable(),
                        source: array,
                        method: nameof(SpanExtensions.Split),
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
                        actual: array.AsSpan().Split(delimiter).ToSystemEnumerable(),
                        source: array,
                        method: nameof(SpanExtensions.Split),
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
                        actual: array.AsSpan().Split(delimiter, count, countExceedingBehaviour).ToSystemEnumerable(),
                        source: array,
                        method: nameof(SpanExtensions.Split),
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

        public sealed class SplitString()
        {
            [Fact]
            public void FuzzSplit()
            {
                static void AssertOptions(string @string, char delimiter, StringSplitOptions options)
                {
                    AssertMethodResults(
                        expected: @string.Split(delimiter, options),
                        actual: @string.ToCharArray().AsSpan().Split(delimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", delimiter), ("options", options)]
                    );
                }

                string @string = GenerateRandomString(length);
                char charDelimiter = @string[random.Next(@string.Length)];
                const char charMissingDelimiter = '!';
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertOptions(@string, charDelimiter, options);
                    AssertOptions(@string, charMissingDelimiter, options);
                }
            }

            [Fact]
            public void FuzzSplitWithCount()
            {
                static void AssertOptions(string @string, char delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                {
                    AssertMethodResults(
                        expected: @string.Split(delimiter, count, options, countExceedingBehaviour),
                        actual: @string.ToCharArray().AsSpan().Split(delimiter, count, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", delimiter), ("count", count), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                string @string = GenerateRandomString(length);
                char charDelimiter = @string[random.Next(@string.Length)];
                int countDelimiters = @string.AsSpan().Count(charDelimiter);
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                    {
                        AssertOptions(@string, charDelimiter, countDelimiters, options, countExceedingBehaviour);
                        AssertOptions(@string, '!', countDelimiters, options, countExceedingBehaviour);
                    }
                }
            }

            [Fact]
            public void FuzzSplitWithDelimiterSequence()
            {
                static void AssertOptions(string @string, char[] delimiter, StringSplitOptions options)
                {
                    AssertMethodResults(
                        expected: @string.Split(new string(delimiter), options),
                        actual: @string.ToCharArray().AsSpan().Split(delimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", delimiter), ("options", options)]
                    );
                }

                string @string = GenerateRandomString(length);
                int startIndex = random.Next(@string.Length - 3);
                char[] charSequenceDelimiter = @string.AsSpan()[startIndex..(startIndex + 3)].ToArray();
                char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceAt(2, '!');
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertOptions(@string, charSequenceDelimiter, options);
                    AssertOptions(@string, charSequenceMissingDelimiter, options);
                }
            }

            [Fact]
            public void FuzzSplitWithDelimiterSequenceAndCount()
            {
                static void AssertOptions(string @string, char[] delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                {
                    AssertMethodResults(
                        expected: @string.Split(new string(delimiter), count, options, countExceedingBehaviour),
                        actual: @string.ToCharArray().AsSpan().Split(delimiter, count, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", delimiter), ("count", count), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                string @string = GenerateRandomString(length);
                int startIndex = random.Next(@string.Length - 3);
                char[] charSequenceDelimiter = @string.AsSpan()[startIndex..(startIndex + 3)].ToArray();
                char[] charSequenceMissingDelimiter = charSequenceDelimiter.ReplaceAt(2, '!');
                int countDelimiters = @string.Count(new string(charSequenceDelimiter));
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                    {
                        AssertOptions(@string, charSequenceDelimiter, countDelimiters, options, countExceedingBehaviour);
                        AssertOptions(@string, charSequenceMissingDelimiter, countDelimiters, options, countExceedingBehaviour);
                    }
                }
            }
        }

        public sealed class SplitAny()
        {
            [Fact]
            public void FuzzSplitAny()
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
                        actual: array.AsSpan().SplitAny(delimiters, count, countExceedingBehaviour).ToSystemEnumerable(),
                        source: array,
                        method: nameof(SpanExtensions.SplitAny),
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

        public sealed class SplitAnyString()
        {
            [Fact]
            public void FuzzSplitAny()
            {
                static void AssertOptions(string @string, char[] delimiters, StringSplitOptions options)
                {
                    AssertMethodResults(
                        expected: @string.Split(delimiters, options),
                        actual: @string.ToCharArray().AsSpan().SplitAny(delimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", delimiters), ("options", options)]
                    );
                }

                string @string = GenerateRandomString(length);
                char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => @string[random.Next(@string.Length)]).ToArray();
                char[] charMissingDelimiters = Enumerable.Range(0, 5).Select(i => (char)('ა' + i)).ToArray();
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertOptions(@string, charDelimiters, options);
                    AssertOptions(@string, charMissingDelimiters, options);
                }
            }

            [Fact]
            public void FuzzSplitAnyWithCount()
            {
                static void AssertOptions(string @string, char[] delimiters, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                {
                    AssertMethodResults(
                        expected: @string.Split(delimiters, count, options, countExceedingBehaviour),
                        actual: @string.ToCharArray().AsSpan().SplitAny(delimiters, count, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", delimiters), ("count", count), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                string @string = GenerateRandomString(length);
                char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => @string[random.Next(@string.Length)]).ToArray();
                char[] charMissingDelimiters = Enumerable.Range(0, 5).Select(i => (char)('ა' + i)).ToArray();
                int countDelimiters = @string.AsSpan().Count(charDelimiters);
                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                    {
                        AssertOptions(@string, charDelimiters, countDelimiters, options, countExceedingBehaviour);
                    }
                }
            }
        }
    }
}