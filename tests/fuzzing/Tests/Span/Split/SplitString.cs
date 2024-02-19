using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitString
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
    }
}
