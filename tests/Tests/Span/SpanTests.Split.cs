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

        public sealed class FuzzTests
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
}