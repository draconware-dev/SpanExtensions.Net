using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        public sealed class SplitAnyString
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
