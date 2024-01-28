using SpanExtensions;
using static Tests.TestHelper;

namespace Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3878:Arrays should not be created for params parameters", Justification = "Readability")]
    public static class ReadOnlySpanTests
    {
        const int count = 250;
        const int minValue = 0;
        const int maxValue = 100;
        const int length = 100;
        static readonly IEnumerable<StringSplitOptions> stringSplitOptions = GetAllStringSplitOptions();

        public static class LinqTests
        {
        }

        public static class SplitTests
        {
            public sealed class RandomData
            {
                [Fact]
                public void TestSplit()
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    ReadOnlySpan<int> integerSpan = integerArray;

                    // source contains delimiter
                    int integerDelimiter = integerArray[random.Next(integerArray.Length)];
                    AssertMethodResults(
                        expected: Split(integerArray, integerDelimiter),
                        actual: integerSpan.Split(integerDelimiter).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", integerDelimiter)
                    );

                    // source does not contain delimiter
                    integerDelimiter = maxValue;
                    AssertMethodResults(
                        expected: Split(integerArray, integerDelimiter),
                        actual: integerSpan.Split(integerDelimiter).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", integerDelimiter)
                    );

                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    ReadOnlySpan<char> charSpan = charArray;

                    // source contains delimiter
                    char charDelimiter = charArray[random.Next(charArray.Length)];
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter),
                        actual: charSpan.Split(charDelimiter).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", charDelimiter)
                    );

                    // source does not contain delimiter
                    charDelimiter = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter),
                        actual: charSpan.Split(charDelimiter).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", charDelimiter)
                    );
                }

                [Fact]
                public void TestSplitWithCount()
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    ReadOnlySpan<int> integerSpan = integerArray;

                    // source contains delimiter
                    int integerDelimiter = integerArray[random.Next(integerArray.Length)];
                    int countDelimiters = integerSpan.Count(integerDelimiter);
                    AssertMethodResults(
                        expected: Split(integerArray, integerDelimiter, countDelimiters),
                        actual: integerSpan.Split(integerDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", integerDelimiter), ("count", countDelimiters)]
                    );

                    // source does not contain delimiter
                    integerDelimiter = maxValue;
                    AssertMethodResults(
                        expected: Split(integerArray, integerDelimiter, countDelimiters),
                        actual: integerSpan.Split(integerDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", integerDelimiter), ("count", countDelimiters)]
                    );

                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    ReadOnlySpan<char> charSpan = charArray;

                    // source contains delimiter
                    char charDelimiter = charArray[random.Next(charArray.Length)];
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters),
                        actual: charSpan.Split(charDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters)]
                    );

                    // source does not contain delimiter
                    charDelimiter = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: Split(charArray, charDelimiter, countDelimiters),
                        actual: charSpan.Split(charDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", charDelimiter), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void TestSplitString()
                {
                    static void AssertOptions(StringSplitOptions options)
                    {
                        string @string = GenerateRandomString(length);
                        ReadOnlySpan<char> charSpan = @string;

                        // source contains delimiter
                        char charDelimiter = @string[random.Next(@string.Length)];
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, options),
                            actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("options", options)]
                        );

                        // source does not contain delimiter
                        charDelimiter = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, options),
                            actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("options", options)]
                        );
                    }

                    foreach(StringSplitOptions _options in stringSplitOptions)
                    {
                        AssertOptions(_options);
                    }
                }

                [Fact]
                public void TestSplitStringWithCount()
                {
                    static void AssertOptions(StringSplitOptions options)
                    {
                        string @string = GenerateRandomString(length);
                        ReadOnlySpan<char> charSpan = @string;

                        // source contains delimiter
                        char charDelimiter = @string[random.Next(@string.Length)];
                        int countDelimiters = charSpan.Count(charDelimiter);
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, countDelimiters, options),
                            actual: charSpan.Split(charDelimiter, countDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options)]
                        );

                        // source does not contain delimiter
                        charDelimiter = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, countDelimiters, options),
                            actual: charSpan.Split(charDelimiter, countDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options)]
                        );
                    }

                    foreach(StringSplitOptions options in stringSplitOptions)
                    {
                        AssertOptions(options);
                    }
                }

                [Fact]
                public void TestSplitAny()
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    ReadOnlySpan<int> integerSpan = integerArray;

                    // source contains delimiter
                    int[] integerDelimiters = Enumerable.Range(0, 5).Select(_ => integerArray[random.Next(integerArray.Length)]).ToArray();
                    AssertMethodResults(
                        expected: SplitAny(integerArray, integerDelimiters),
                        actual: integerSpan.SplitAny(integerDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: ("delimiters", integerDelimiters)
                    );

                    // source does not contain delimiter
                    integerDelimiters = Enumerable.Range(0, 5).Select(i => maxValue + i).ToArray();
                    AssertMethodResults(
                        expected: SplitAny(integerArray, integerDelimiters),
                        actual: integerSpan.SplitAny(integerDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: ("delimiters", integerDelimiters)
                    );

                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    ReadOnlySpan<char> charSpan = charArray;

                    // source contains delimiter
                    char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => charArray[random.Next(charArray.Length)]).ToArray();
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters),
                        actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: ("delimiters", charDelimiters)
                    );

                    // source does not contain delimiter
                    charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters),
                        actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: ("delimiters", charDelimiters)
                    );
                }

                [Fact]
                public void TestSplitAnyWithCount()
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    ReadOnlySpan<int> integerSpan = integerArray;

                    // source contains delimiter
                    int[] integerDelimiters = Enumerable.Range(0, 5).Select(_ => integerArray[random.Next(integerArray.Length)]).ToArray();
                    int countDelimiters = integerSpan.Count(integerDelimiters);
                    AssertMethodResults(
                        expected: SplitAny(integerArray, integerDelimiters, countDelimiters),
                        actual: integerSpan.SplitAny(integerDelimiters, countDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: [("delimiters", integerDelimiters), ("count", countDelimiters)]
                    );

                    // source does not contain delimiter
                    integerDelimiters = Enumerable.Range(0, 5).Select(i => maxValue + i).ToArray();
                    AssertMethodResults(
                        expected: SplitAny(integerArray, integerDelimiters, countDelimiters),
                        actual: integerSpan.SplitAny(integerDelimiters, countDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: [("delimiters", integerDelimiters), ("count", countDelimiters)]
                    );

                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    ReadOnlySpan<char> charSpan = charArray;

                    // source contains delimiter
                    char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => charArray[random.Next(charArray.Length)]).ToArray();
                    countDelimiters = charSpan.Count(charDelimiters);
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters)]
                    );

                    // source does not contain delimiter
                    charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: SplitAny(charArray, charDelimiters, countDelimiters),
                        actual: charSpan.SplitAny(charDelimiters, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.SplitAny),
                        args: [("delimiters", charDelimiters), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void TestSplitAnyString()
                {
                    static void AssertOptions(StringSplitOptions options)
                    {
                        string @string = GenerateRandomString(length);
                        ReadOnlySpan<char> charSpan = @string;

                        // source contains delimiter
                        char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => @string[random.Next(@string.Length)]).ToArray();
                        AssertMethodResults(
                            expected: @string.Split(charDelimiters, options),
                            actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.SplitAny),
                            args: [("delimiters", charDelimiters), ("options", options)]
                        );

                        // source does not contain delimiter
                        charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters, numbers, and white spaces
                        AssertMethodResults(
                            expected: @string.Split(charDelimiters, options),
                            actual: charSpan.SplitAny(charDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.SplitAny),
                            args: [("delimiters", charDelimiters), ("options", options)]
                        );
                    }

                    foreach(StringSplitOptions _options in stringSplitOptions)
                    {
                        AssertOptions(_options);
                    }
                }

                [Fact]
                public void TestSplitAnyStringWithCount()
                {
                    static void AssertOptions(StringSplitOptions options)
                    {
                        string @string = GenerateRandomString(length);
                        ReadOnlySpan<char> charSpan = @string;

                        // source contains delimiter
                        char[] charDelimiters = Enumerable.Range(0, 5).Select(_ => @string[random.Next(@string.Length)]).ToArray();
                        int countDelimiters = charSpan.Count(charDelimiters);
                        AssertMethodResults(
                            expected: @string.Split(charDelimiters, countDelimiters, options),
                            actual: charSpan.SplitAny(charDelimiters, countDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.SplitAny),
                            args: [("delimiters", charDelimiters), ("options", options), ("count", countDelimiters)]
                        );

                        // source does not contain delimiter
                        charDelimiters = Enumerable.Range(0, 5).Select(_ => '!').ToArray(); // the generated array only consists of lowercase letters, numbers, and white spaces
                        AssertMethodResults(
                            expected: @string.Split(charDelimiters, countDelimiters, options),
                            actual: charSpan.SplitAny(charDelimiters, countDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.SplitAny),
                            args: [("delimiters", charDelimiters), ("options", options), ("count", countDelimiters)]
                        );
                    }

                    foreach(StringSplitOptions _options in stringSplitOptions)
                    {
                        AssertOptions(_options);
                    }
                }

                [Fact]
                public void TestSplitSequence()
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    ReadOnlySpan<int> integerSpan = integerArray;

                    // source contains delimiter
                    int startIndex = random.Next(integerArray.Length - 3);
                    int[] integerSequenceDelimiter = integerArray[startIndex..(startIndex + 3)];
                    AssertMethodResults(
                        expected: Split(integerArray, integerSequenceDelimiter),
                        actual: integerSpan.Split(integerSequenceDelimiter).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", integerSequenceDelimiter)
                    );

                    // source does not contain delimiter
                    integerSequenceDelimiter[^1] = maxValue;
                    AssertMethodResults(
                        expected: Split(integerArray, integerSequenceDelimiter),
                        actual: integerSpan.Split(integerSequenceDelimiter).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", integerSequenceDelimiter)
                    );

                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    ReadOnlySpan<char> charSpan = charArray;

                    // source contains delimiter
                    startIndex = random.Next(charArray.Length - 3);
                    char[] charSequenceDelimiter = charArray[startIndex..(startIndex + 3)];
                    AssertMethodResults(
                        expected: Split(charArray, charSequenceDelimiter),
                        actual: charSpan.Split(charSequenceDelimiter).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", charSequenceDelimiter)
                    );

                    // source does not contain delimiter
                    charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: Split(charArray, charSequenceDelimiter),
                        actual: charSpan.Split(charSequenceDelimiter).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: ("delimiter", charSequenceDelimiter)
                    );
                }

                [Fact]
                public void TestSplitSequenceWithCount()
                {
                    int[] integerArray = GenerateRandomIntegers(count, minValue, maxValue).ToArray();
                    ReadOnlySpan<int> integerSpan = integerArray;

                    // source contains delimiter
                    int startIndex = random.Next(integerArray.Length - 3);
                    int[] integerSequenceDelimiter = integerArray[startIndex..(startIndex + 3)];
                    int countDelimiters = integerSpan.CountSequence(integerSequenceDelimiter);
                    AssertMethodResults(
                        expected: Split(integerArray, integerSequenceDelimiter, countDelimiters),
                        actual: integerSpan.Split(integerSequenceDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", integerSequenceDelimiter), ("count", countDelimiters)]
                    );

                    // source does not contain delimiter
                    integerSequenceDelimiter[^1] = maxValue;
                    AssertMethodResults(
                        expected: Split(integerArray, integerSequenceDelimiter, countDelimiters),
                        actual: integerSpan.Split(integerSequenceDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: integerArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", integerSequenceDelimiter), ("count", countDelimiters)]
                    );

                    char[] charArray = GenerateRandomString(length).ToCharArray();
                    ReadOnlySpan<char> charSpan = charArray;

                    // source contains delimiter
                    startIndex = random.Next(charArray.Length - 3);
                    char[] charSequenceDelimiter = charArray[startIndex..(startIndex + 3)];
                    countDelimiters = charSpan.CountSequence(charSequenceDelimiter);
                    AssertMethodResults(
                        expected: Split(charArray, charSequenceDelimiter, countDelimiters),
                        actual: charSpan.Split(charSequenceDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("count", countDelimiters)]
                    );

                    // source does not contain delimiter
                    charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                    AssertMethodResults(
                        expected: Split(charArray, charSequenceDelimiter, countDelimiters),
                        actual: charSpan.Split(charSequenceDelimiter, countDelimiters).ToSystemEnumerable(),
                        source: charArray,
                        method: nameof(ReadOnlySpanExtensions.Split),
                        args: [("delimiter", charSequenceDelimiter), ("count", countDelimiters)]
                    );
                }

                [Fact]
                public void TestSplitSequenceString()
                {
                    static void AssertOptions(StringSplitOptions options)
                    {
                        string @string = GenerateRandomString(length);
                        ReadOnlySpan<char> charSpan = @string;

                        // source contains delimiter
                        int startIndex = random.Next(@string.Length - 3);
                        char[] charSequenceDelimiter = @string.AsSpan()[startIndex..(startIndex + 3)].ToArray();
                        AssertMethodResults(
                            expected: @string.Split(new string(charSequenceDelimiter), options),
                            actual: charSpan.Split(charSequenceDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charSequenceDelimiter), ("options", options)]
                        );

                        // source does not contain delimiter
                        charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                        AssertMethodResults(
                            expected: @string.Split(new string(charSequenceDelimiter), options),
                            actual: charSpan.Split(charSequenceDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charSequenceDelimiter), ("options", options)]
                        );
                    }

                    foreach(StringSplitOptions _options in stringSplitOptions)
                    {
                        AssertOptions(_options);
                    }
                }

                [Fact]
                public void TestSplitSequenceStringWithCount()
                {
                    static void AssertOptions(StringSplitOptions options)
                    {
                        string @string = GenerateRandomString(length);
                        ReadOnlySpan<char> charSpan = @string;

                        // source contains delimiter
                        int startIndex = random.Next(@string.Length - 3);
                        char[] charSequenceDelimiter = @string.AsSpan()[startIndex..(startIndex + 3)].ToArray();
                        int countDelimiters = @string.Count(new string(charSequenceDelimiter));
                        AssertMethodResults(
                            expected: @string.Split(new string(charSequenceDelimiter), countDelimiters, options),
                            actual: charSpan.Split(charSequenceDelimiter, countDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charSequenceDelimiter), ("options", options), ("count", countDelimiters)]
                        );

                        // source does not contain delimiter
                        charSequenceDelimiter[^1] = '!'; // the generated array only consists of lowercase letters, numbers, and white spaces
                        AssertMethodResults(
                            expected: @string.Split(new string(charSequenceDelimiter), countDelimiters, options),
                            actual: charSpan.Split(charSequenceDelimiter, countDelimiters, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charSequenceDelimiter), ("options", options), ("count", countDelimiters)]
                        );
                    }

                    foreach(StringSplitOptions _options in stringSplitOptions)
                    {
                        AssertOptions(_options);
                    }
                }
            }

            public static class Facts
            {
                public sealed class SplitString
                {
                    [Fact]
                    public void ConsecutiveDelimitersResultInEmptySpan()
                    {
                        const string @string = "abba";
                        ReadOnlySpan<char> charSpan = @string;
                        const char charDelimiter = 'b';
                        const StringSplitOptions options = StringSplitOptions.None;
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, options),
                            actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("options", options)]
                        );
                    }

                    [Fact]
                    public void DelimiterAtTheEndResultInEmptySpan()
                    {
                        const string @string = "aab";
                        ReadOnlySpan<char> charSpan = @string;
                        const char charDelimiter = 'b';
                        const StringSplitOptions options = StringSplitOptions.None;
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, options),
                            actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("options", options)]
                        );
                    }

                    [Fact]
                    public void DelimiterAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpan()
                    {
                        const string @string = "aab";
                        ReadOnlySpan<char> charSpan = @string.ToCharArray();
                        const char charDelimiter = 'b';
                        const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, options),
                            actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("options", options)]
                        );
                    }

                    [Fact]
                    public void ConsecutiveDelimitersAtTheEndWithRemoveEmptyEntriesOptionResultInNoEmptySpans()
                    {
                        const string @string = "aabb";
                        ReadOnlySpan<char> charSpan = @string.ToCharArray();
                        const char charDelimiter = 'b';
                        const StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries;
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, options),
                            actual: charSpan.Split(charDelimiter, options).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("options", options)]
                        );
                    }
                }

                public sealed class SplitAny
                {
                    [Fact]
                    public void ConsecutiveDelimitersResultInEmptySpan()
                    {
                        const string charArray = "abca";
                        ReadOnlySpan<char> charSpan = charArray;
                        char[] charDelimiters = ['b', 'c'];
                        AssertMethodResults(
                            expected: SplitAny(charArray, charDelimiters),
                            actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                            source: charArray,
                            method: nameof(ReadOnlySpanExtensions.SplitAny),
                            args: ("delimiters", charDelimiters)
                        );
                    }

                    [Fact]
                    public void DelimiterAtTheEndResultInEmptySpan()
                    {
                        const string charArray = "aac";
                        ReadOnlySpan<char> charSpan = charArray;
                        char[] charDelimiters = ['b', 'c'];
                        AssertMethodResults(
                            expected: SplitAny(charArray, charDelimiters),
                            actual: charSpan.SplitAny(charDelimiters).ToSystemEnumerable(),
                            source: charArray,
                            method: nameof(ReadOnlySpanExtensions.SplitAny),
                            args: ("delimiters", charDelimiters)
                        );
                    }
                }
            }
        }

        public static class StringTests
        {
        }
    }
}