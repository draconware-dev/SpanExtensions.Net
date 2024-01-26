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
                    charDelimiter = '!'; // the generated array only consists of lowercase letters and numbers
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
                    charDelimiter = '!'; // the generated array only consists of lowercase letters and numbers
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
                        charDelimiter = '!'; // the generated array only consists of lowercase letters and numbers
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
                            actual: charSpan.Split(charDelimiter, options, countDelimiters).ToSystemEnumerable(),
                            source: @string,
                            method: nameof(ReadOnlySpanExtensions.Split),
                            args: [("delimiter", charDelimiter), ("count", countDelimiters), ("options", options)]
                        );

                        // source does not contain delimiter
                        charDelimiter = '!'; // the generated array only consists of lowercase letters and numbers
                        AssertMethodResults(
                            expected: @string.Split(charDelimiter, countDelimiters, options),
                            actual: charSpan.Split(charDelimiter, options, countDelimiters).ToSystemEnumerable(),
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
                }
            }
        }

        public static class StringTests
        {
        }
    }
}