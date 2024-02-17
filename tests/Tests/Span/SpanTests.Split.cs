using static SpanExtensions.Testing.TestHelper;

namespace SpanExtensions.Testing
{
    public static class SpanSplitTests
    {
        const int count = 250;
        const int minValue = 0;
        const int maxValue = 100;
        const int length = 100;
        static readonly IEnumerable<StringSplitOptions> stringSplitOptions = GetAllStringSplitOptions();
        static readonly CountExceedingBehaviour[] countExceedingBehaviours = (CountExceedingBehaviour[])Enum.GetValues(typeof(CountExceedingBehaviour));

        public sealed class MonkeyTests
        {
            [Fact]
            public void TestSplit()
            {
                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                Span<int> integerSpan = integerArray;

                // source contains delimiter
                int integerDelimiter = integerArray[random.Next(integerArray.Length)];
                AssertMethodResults(
                    expected: Split(integerArray, integerDelimiter),
                    actual: integerSpan.Split(integerDelimiter).ToSystemEnumerable(),
                    source: integerArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", integerDelimiter)
                );

                // source does not contain delimiter
                integerDelimiter = maxValue;
                AssertMethodResults(
                    expected: Split(integerArray, integerDelimiter),
                    actual: integerSpan.Split(integerDelimiter).ToSystemEnumerable(),
                    source: integerArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", integerDelimiter)
                );

                char[] charArray = GenerateRandomString(length).ToCharArray();
                Span<char> charSpan = charArray;

                // source contains delimiter
                char charDelimiter = charArray[random.Next(charArray.Length)];
                AssertMethodResults(
                    expected: Split(charArray, charDelimiter),
                    actual: charSpan.Split(charDelimiter).ToSystemEnumerable(),
                    source: charArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", charDelimiter)
                );

                // source does not contain delimiter
                charDelimiter = '!'; // the generated array only consists of lowercase letters and numbers
                AssertMethodResults(
                    expected: Split(charArray, charDelimiter),
                    actual: charSpan.Split(charDelimiter).ToSystemEnumerable(),
                    source: charArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", charDelimiter)
                );
            }

            [Fact]
            public void TestSplitWithCount()
            {
                static void AssertIntOptions(CountExceedingBehaviour countExceedingBehaviour)
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    Span<int> integerSpan = integerArray;

                    // source contains delimiter
                    int integerDelimiter = integerArray[random.Next(integerArray.Length)];
                    int countDelimiters = integerSpan.Count(integerDelimiter);
                    AssertMethodResults(
                        expected: Split(integerArray, integerDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: integerSpan.Split(integerDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", integerDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    integerDelimiter = maxValue;
                    AssertMethodResults(
                        expected: Split(integerArray, integerDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: integerSpan.Split(integerDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", integerDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertIntOptions(countExceedingBehaviour);
                }

                static void AssertCharOptions(CountExceedingBehaviour countExceedingBehaviour)
                {
                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    Span<char> charSpan = charArray;

                    // source contains delimiter
                    char charDelimiter = charArray[random.Next(charArray.Length)];
                    int countDelimiters = charSpan.Count(charDelimiter);
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.Split(charDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    charDelimiter = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters),
                        actual: charSpan.Split(charDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertCharOptions(countExceedingBehaviour);
                }
            }

            [Fact]
            public void TestSplitString()
            {
                static void AssertOptions(StringSplitOptions options)
                {
                    string @string = GenerateRandomString(length);
                    Span<char> charSpan = @string.ToCharArray();

                    // source contains delimiter
                    char charDelimiter = @string[random.Next(@string.Length)];
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );

                    // source does not contain delimiter
                    charDelimiter = '!'; // the generated array only consists of lowercase letters and numbers
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertOptions(options);
                }
            }

            [Fact]
            public void TestSplitStringWithCount()
            {
                static void AssertOptions(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                {
                    string @string = GenerateRandomString(length);
                    Span<char> charSpan = @string.ToCharArray();

                    // source contains delimiter
                    char charDelimiter = @string[random.Next(@string.Length)];
                    int countDelimiters = charSpan.Count(charDelimiter);
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.Split(charDelimiter, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    charDelimiter = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.Split(charDelimiter, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }
            }

            [Fact]
            public void TestSplitAny()
            {
                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                Span<int> integerSpan = integerArray;

                // source contains delimiter
                int[] integerDelimiters = Enumerable.Range(0, 5).Select(_ => integerArray[random.Next(integerArray.Length)]).ToArray();
                AssertMethodResults(
                    expected: SplitAny(integerArray, integerDelimiters),
                    actual: integerSpan.SplitAny(integerDelimiters).ToSystemEnumerable(),
                    source: integerArray,
                    method: nameof(SpanExtensions.SplitAny),
                    args: ("delimiters", integerDelimiters)
                );

                // source does not contain delimiter
                integerDelimiters = Enumerable.Range(0, 5).Select(i => maxValue + i).ToArray();
                AssertMethodResults(
                    expected: SplitAny(integerArray, integerDelimiters),
                    actual: integerSpan.SplitAny(integerDelimiters).ToSystemEnumerable(),
                    source: integerArray,
                    method: nameof(SpanExtensions.SplitAny),
                    args: ("delimiters", integerDelimiters)
                );

                char[] charArray = GenerateRandomString(length).ToCharArray();
                Span<char> charSpan = charArray;

                // source contains delimiter
                char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => charArray[random.Next(charArray.Length)]).ToArray();
                AssertMethodResults(
                    expected: SplitAny(charArray, charDelimiters),
                    actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                    source: charArray,
                    method: nameof(SpanExtensions.SplitAny),
                    args: ("delimiters", charDelimiters)
                );

                // source does not contain delimiter
                charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters and numbers
                AssertMethodResults(
                    expected: SplitAny(charArray, charDelimiters),
                    actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                    source: charArray,
                    method: nameof(SpanExtensions.SplitAny),
                    args: ("delimiters", charDelimiters)
                );
            }

            [Fact]
            public void TestSplitAnyWithCount()
            {
                static void AssertIntOptions(CountExceedingBehaviour countExceedingBehaviour)
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    Span<int> integerSpan = integerArray;

                    // source contains delimiter
                    int[] integerDelimiters = Enumerable.Range(0, 5).Select(_ => integerArray[random.Next(integerArray.Length)]).ToArray();
                    int countDelimiters = integerSpan.Count(integerDelimiters);
                    AssertMethodResults(
                        expected: SplitAny(integerArray, integerDelimiters, countDelimiters, countExceedingBehaviour),
                        actual: integerSpan.SplitAny(integerDelimiters, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", integerDelimiters), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    integerDelimiters = Enumerable.Range(0, 5).Select(i => maxValue + i).ToArray();
                    AssertMethodResults(
                        expected: SplitAny(integerArray, integerDelimiters, countDelimiters, countExceedingBehaviour),
                        actual: integerSpan.SplitAny(integerDelimiters, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", integerDelimiters), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertIntOptions(countExceedingBehaviour);
                }

                static void AssertCharOptions(CountExceedingBehaviour countExceedingBehaviour)
                {
                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    Span<char> charSpan = charArray;

                    // source contains delimiter
                    char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => charArray[random.Next(charArray.Length)]).ToArray();
                    int countDelimiters = charSpan.Count(charDelimiters);
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertCharOptions(countExceedingBehaviour);
                }
            }

            [Fact]
            public void TestSplitAnyString()
            {
                static void AssertOptions(StringSplitOptions options)
                {
                    string @string = GenerateRandomString(length);
                    Span<char> charSpan = @string.ToCharArray();

                    // source contains delimiter
                    char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => @string[random.Next(@string.Length)]).ToArray();
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );

                    // source does not contain delimiter
                    charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters and numbers
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertOptions(options);
                }
            }

            [Fact]
            public void TestSplitAnyStringWithCount()
            {
                static void AssertOptions(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                {
                    string @string = GenerateRandomString(length);
                    Span<char> charSpan = @string.ToCharArray();

                    // source contains delimiter
                    char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => @string[random.Next(@string.Length)]).ToArray();
                    int countDelimiters = charSpan.Count(charDelimiters);
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                    {
                        AssertOptions(options, countExceedingBehaviour);
                    }
                }
            }

            [Fact]
            public void TestSplitSequence()
            {
                int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                Span<int> integerSpan = integerArray;

                // source contains delimiter
                int startIndex = random.Next(integerArray.Length - 3);
                int[] integerSequenceDelimiter = integerArray[startIndex..(startIndex + 3)];
                AssertMethodResults(
                    expected: Split(integerArray, integerSequenceDelimiter),
                    actual: integerSpan.Split(integerSequenceDelimiter).ToSystemEnumerable(),
                    source: integerArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", integerSequenceDelimiter)
                );

                // source does not contain delimiter
                integerSequenceDelimiter[^1] = maxValue;
                AssertMethodResults(
                    expected: Split(integerArray, integerSequenceDelimiter),
                    actual: integerSpan.Split(integerSequenceDelimiter).ToSystemEnumerable(),
                    source: integerArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", integerSequenceDelimiter)
                );

                char[] charArray = GenerateRandomString(length).ToCharArray();
                Span<char> charSpan = charArray;

                // source contains delimiter
                startIndex = random.Next(charArray.Length - 3);
                char[] charSequenceDelimiter = charArray[startIndex..(startIndex + 3)];
                AssertMethodResults(
                    expected: Split(charArray, charSequenceDelimiter),
                    actual: charSpan.Split(charSequenceDelimiter).ToSystemEnumerable(),
                    source: charArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", charSequenceDelimiter)
                );

                // source does not contain delimiter
                charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters and numbers
                AssertMethodResults(
                    expected: Split(charArray, charSequenceDelimiter),
                    actual: charSpan.Split(charSequenceDelimiter).ToSystemEnumerable(),
                    source: charArray,
                    method: nameof(SpanExtensions.Split),
                    args: ("delimiter", charSequenceDelimiter)
                );
            }

            [Fact]
            public void TestSplitSequenceWithCount()
            {
                static void AssertIntOptions(CountExceedingBehaviour countExceedingBehaviour)
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    Span<int> integerSpan = integerArray;

                    // source contains delimiter
                    int startIndex = random.Next(integerArray.Length - 3);
                    int[] integerSequenceDelimiter = integerArray[startIndex..(startIndex + 3)];
                    int countDelimiters = integerSpan.CountSequence(integerSequenceDelimiter);
                    AssertMethodResults(
                        expected: Split(integerArray, integerSequenceDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: integerSpan.Split(integerSequenceDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", integerSequenceDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    integerSequenceDelimiter[^1] = maxValue;
                    AssertMethodResults(
                        expected: Split(integerArray, integerSequenceDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: integerSpan.Split(integerSequenceDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", integerSequenceDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertIntOptions(countExceedingBehaviour);
                }

                static void AssertCharOptions(CountExceedingBehaviour countExceedingBehaviour)
                {
                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    Span<char> charSpan = charArray;

                    // source contains delimiter
                    int startIndex = random.Next(charArray.Length - 3);
                    char[] charSequenceDelimiter = charArray[startIndex..(startIndex + 3)];
                    int countDelimiters = charSpan.CountSequence(charSequenceDelimiter);
                    AssertMethodResults(
                        expected: Split(charArray, charSequenceDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.Split(charSequenceDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: Split(charArray, charSequenceDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.Split(charSequenceDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                {
                    AssertCharOptions(countExceedingBehaviour);
                }
            }

            [Fact]
            public void TestSplitSequenceString()
            {
                static void AssertOptions(StringSplitOptions options)
                {
                    string @string = GenerateRandomString(length);
                    Span<char> charSpan = @string.ToCharArray();

                    // source contains delimiter
                    int startIndex = random.Next(@string.Length - 3);
                    char[] charSequenceDelimiter = @string.AsSpan()[startIndex..(startIndex + 3)].ToArray();
                    AssertMethodResults(
                        expected: @string.Split(new string(charSequenceDelimiter), options),
                        actual: charSpan.Split(charSequenceDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("options", options)]
                    );

                    // source does not contain delimiter
                    charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters and numbers
                    AssertMethodResults(
                        expected: @string.Split(new string(charSequenceDelimiter), options),
                        actual: charSpan.Split(charSequenceDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("options", options)]
                    );
                }

                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    AssertOptions(options);
                }
            }

            [Fact]
            public void TestSplitSequenceStringWithCount()
            {
                static void AssertOptions(StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour)
                {
                    string @string = GenerateRandomString(length);
                    Span<char> charSpan = @string.ToCharArray();

                    // source contains delimiter
                    int startIndex = random.Next(@string.Length - 3);
                    char[] charSequenceDelimiter = @string.AsSpan()[startIndex..(startIndex + 3)].ToArray();
                    int countDelimiters = @string.Count(new string(charSequenceDelimiter));
                    AssertMethodResults(
                        expected: @string.Split(new string(charSequenceDelimiter), countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.Split(charSequenceDelimiter, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );

                    // source does not contain delimiter
                    charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: @string.Split(new string(charSequenceDelimiter), countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.Split(charSequenceDelimiter, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                foreach(StringSplitOptions options in stringSplitOptions)
                {
                    foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                    {
                        AssertOptions(options, countExceedingBehaviour);
                    }
                }
            }
        }

        public static class Facts
        {
            public sealed class Split
            {
                [Fact]
                public void ConsecutiveDelimitersResultInEmptySpan()
                {
                    const string charArray = "abba";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter),
                        actual: charSpan.Split(charDelimiter).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: ("delimiter", charDelimiter)
                    );
                }

                [Fact]
                public void DelimiterAtTheEndResultInEmptySpan()
                {
                    const string charArray = "aab";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter),
                        actual: charSpan.Split(charDelimiter).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: ("delimiter", charDelimiter)
                    );
                }

                [Fact]
                public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
                {
                    const string charArray = "aab";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters),
                        actual: charSpan.Split(charDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
                {
                    const string charArray = "aabaa";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters),
                        actual: charSpan.Split(charDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
                {
                    const string charArray = "aabaa";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    const CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.CutLastElements;
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.Split(charDelimiter, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                [Fact]
                public void CountEqualZeroReturnsNothing()
                {
                    const string charArray = "aabb";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    const int count = 0;
                    AssertMethodResults(
                        expected: [],
                        actual: charSpan.Split(charDelimiter, count).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", count)]
                    );
                }

                [Fact]
                public void NegativeCountThrowsArgumentOutOfRangeException()
                {
                    const string @string = "aabb";
                    const char charDelimiter = 'b';
                    const int count = -1;
                    Assert.Throws<ArgumentOutOfRangeException>(() => @string.ToCharArray().AsSpan().Split(charDelimiter, count).ToSystemEnumerable());
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
                {
                    const string charArray = "bbaa";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: [charArray],
                        actual: charSpan.Split(charDelimiter, count, options).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", count), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
                {
                    const string charArray = " b b aa";
                    Span<char> charSpan = charArray.ToCharArray();
                    const char charDelimiter = 'b';
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: [charArray.Trim()],
                        actual: charSpan.Split(charDelimiter, count, options).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", count), ("options", options)]
                    );
                }
            }

            public sealed class SplitString
            {
                [Fact]
                public void ConsecutiveDelimitersResultInEmptySpan()
                {
                    const string @string = "abba";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void DelimiterAtTheEndResultInEmptySpan()
                {
                    const string @string = "aab";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
                {
                    const string @string = "aab";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, countDelimiters, options),
                        actual: charSpan.Split(charDelimiter, countDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
                {
                    const string @string = "aabaa";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, countDelimiters, options),
                        actual: charSpan.Split(charDelimiter, countDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
                {
                    const string @string = "aabaa";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    const StringSplitOptions options = StringSplitOptions.None;
                    const CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.CutLastElements;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.Split(charDelimiter, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                [Fact]
                public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
                {
                    const string @string = "aab";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
                {
                    const string @string = "aabb";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    int countDelimiters = charSpan.Count(charDelimiter);
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, countDelimiters, options),
                        actual: charSpan.Split(charDelimiter, countDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void TrimEntriesOptionTrimsLastSpan()
                {
                    const string @string = " a b a ";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const StringSplitOptions options = StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void EmptySpanWithRemoveEmptyEntriesOptionReturnsNothing()
                {
                    const string @string = "";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = '_';
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
                {
                    const string @string = "  ";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = '_';
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
                {
                    const string @string = "aabb";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, options),
                        actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualZeroReturnsNothing()
                {
                    const string @string = "aabb";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const int count = 0;
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, count, options),
                        actual: charSpan.Split(charDelimiter, count, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", count), ("options", options)]
                    );
                }

                [Fact]
                public void NegativeCountThrowsArgumentOutOfRangeException()
                {
                    const string @string = "aabb";
                    const char charDelimiter = 'b';
                    const int count = -1;
                    const StringSplitOptions options = StringSplitOptions.None;
                    Assert.Throws<ArgumentOutOfRangeException>(() => @string.Split(charDelimiter, count, options));
                    Assert.Throws<ArgumentOutOfRangeException>(() => @string.ToCharArray().AsSpan().Split(charDelimiter, count, options).ToSystemEnumerable());
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
                {
                    const string @string = "bbaa";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, count, options),
                        actual: charSpan.Split(charDelimiter, count, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", count), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
                {
                    const string @string = " b b aa";
                    Span<char> charSpan = @string.ToCharArray();
                    const char charDelimiter = 'b';
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiter, count, options),
                        actual: charSpan.Split(charDelimiter, count, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", count), ("options", options)]
                    );
                }
            }

            public sealed class SplitAny
            {
                [Fact]
                public void ConsecutiveDelimitersResultInEmptySpan()
                {
                    const string charArray = "abca";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters),
                        actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: ("delimiters", charDelimiters)
                    );
                }

                [Fact]
                public void DelimiterAtTheEndResultInEmptySpan()
                {
                    const string charArray = "aac";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters),
                        actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: ("delimiters", charDelimiters)
                    );
                }

                [Fact]
                public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
                {
                    const string charArray = "aac";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
                {
                    const string charArray = "aabaa";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
                {
                    const string charArray = "aabaa";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    const CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.CutLastElements;
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters, countExceedingBehaviour),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, countExceedingBehaviour).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                [Fact]
                public void CountEqualZeroReturnsNothing()
                {
                    const string charArray = "aabc";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const int count = 0;
                    AssertMethodResults(
                        expected: [],
                        actual: charSpan.SplitAny(charDelimiters, count).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", count)]
                    );
                }

                [Fact]
                public void NegativeCountThrowsArgumentOutOfRangeException()
                {
                    const string @string = "aabc";
                    char[] charDelimiters = ['b', 'c'];
                    const int count = -1;
                    Assert.Throws<ArgumentOutOfRangeException>(() => @string.ToCharArray().AsSpan().SplitAny(charDelimiters, count).ToSystemEnumerable());
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
                {
                    const string charArray = "bcaa";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: [charArray],
                        actual: charSpan.SplitAny(charDelimiters, count, options).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiter", charDelimiters), ("count", count), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
                {
                    const string charArray = " b c aa";
                    Span<char> charSpan = charArray.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: [charArray.Trim()],
                        actual: charSpan.SplitAny(charDelimiters, count, options).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiter", charDelimiters), ("count", count), ("options", options)]
                    );
                }
            }

            public sealed class SplitAnyString
            {
                [Fact]
                public void ConsecutiveDelimitersResultInEmptySpan()
                {
                    const string @string = "abca";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void DelimiterAtTheEndResultInEmptySpan()
                {
                    const string @string = "aac";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void DelimiterAtTheEndWithCountEqualDelimiterCountResultsInSpanWithDelimiter()
                {
                    const string @string = "aac";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, countDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInSpanWithEverythingAfterAndIncludingLastDelimiter()
                {
                    const string @string = "aabaa";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, countDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualDelimiterCountResultsInEverythingAfterAndIncludingLastDelimiterBeingCut()
                {
                    const string @string = "aabaa";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    const StringSplitOptions options = StringSplitOptions.None;
                    const CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.CutLastElements;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, countDelimiters, options, countExceedingBehaviour),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, options, countExceedingBehaviour).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("options", options), ("countExceedingBehaviour", countExceedingBehaviour)]
                    );
                }

                [Fact]
                public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
                {
                    const string @string = "aac";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void ConsecutiveDelimitersAtTheEndWithCountEqualDelimiterCountWithRemoveEmptyEntriesOptionResultInNoSpanWithDelimiter()
                {
                    const string @string = "aabc";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    int countDelimiters = charSpan.Count(charDelimiters);
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, countDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void WhiteSpaceCharactersAssumedWhenDelimitersCollectionIsEmpty()
                {
                    const string @string = "a b c d";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = [];
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void EmptySpanWithRemoveEmptyEntriesOptionReturnsNothing()
                {
                    const string @string = "";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['_', '!'];
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void WhiteSpaceSpanWithTrimEntriesAndRemoveEmptyEntriesOptionsReturnsNothing()
                {
                    const string @string = "  ";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['_', '!'];
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
                {
                    const string @string = "aabc";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, options),
                        actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualZeroReturnsNothing()
                {
                    const string @string = "aabc";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const int count = 0;
                    const StringSplitOptions options = StringSplitOptions.None;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, count, options),
                        actual: charSpan.SplitAny(charDelimiters, count, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", count), ("options", options)]
                    );
                }

                [Fact]
                public void NegativeCountThrowsArgumentOutOfRangeException()
                {
                    const string @string = "aabc";
                    char[] charDelimiters = ['b', 'c'];
                    const int count = -1;
                    const StringSplitOptions options = StringSplitOptions.None;
                    Assert.Throws<ArgumentOutOfRangeException>(() => @string.Split(charDelimiters, count, options));
                    Assert.Throws<ArgumentOutOfRangeException>(() => @string.ToCharArray().AsSpan().SplitAny(charDelimiters, count, options).ToSystemEnumerable());
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesOptionDoesNotRecursivelyRemoveEmptySpansAtTheStart()
                {
                    const string @string = "bcaa";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, count, options),
                        actual: charSpan.SplitAny(charDelimiters, count, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiter", charDelimiters), ("count", count), ("options", options)]
                    );
                }

                [Fact]
                public void CountEqualOneWithRemoveEmptyEntriesAndTrimEntriesOptionsDoesNotRecursivelyRemoveWhiteSpaceSpansAtTheStart()
                {
                    const string @string = " b c aa";
                    Span<char> charSpan = @string.ToCharArray();
                    char[] charDelimiters = ['b', 'c'];
                    const int count = 1;
                    const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    AssertMethodResults(
                        expected: @string.Split(charDelimiters, count, options),
                        actual: charSpan.SplitAny(charDelimiters, count, options).ToSystemEnumerable(),
                        source: @string,
                        method: nameof(SpanExtensions.SplitAny),
                        args: [("delimiter", charDelimiters), ("count", count), ("options", options)]
                    );
                }
            }
        }
    }
}