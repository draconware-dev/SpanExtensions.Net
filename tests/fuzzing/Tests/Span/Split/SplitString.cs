using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        public static class SplitString
        {
            public static TheoryData<int, int> SplitData(int iterations)
            {
                TheoryData<int, int> data = new();

                foreach(int length in new MultiplierRange(1, 1000, 10).And([0]))
                {
                    data.Add(iterations, length);
                }

                return data;
            }

            public sealed class SplitWithoutParameters
            {
                public static readonly TheoryData<int, int> _SplitData = SplitData(125000);

                [Theory]
                [MemberData(nameof(_SplitData))]
                public void Fuzz(int iterations, int length)
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

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        string @string = GenerateRandomString(length);
                        char charDelimiter = @string.RandomElementOrDefault('0');
                        const char charMissingDelimiter = 'ა';
                        foreach(StringSplitOptions options in stringSplitOptions)
                        {
                            AssertOptions(@string, charDelimiter, options);
                            AssertOptions(@string, charMissingDelimiter, options);
                        }
                    }
                }
            }

            public sealed class SplitWithCount
            {
                public static readonly TheoryData<int, int> _SplitData = SplitData(20000);

                [Theory]
                [MemberData(nameof(_SplitData))]
                public void Fuzz(int iterations, int length)
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

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        string @string = GenerateRandomString(length);
                        char charDelimiter = @string.RandomElementOrDefault('0');
                        int countDelimiters = @string.Count(charDelimiter);
                        foreach(StringSplitOptions options in stringSplitOptions)
                        {
                            foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                            {
                                AssertOptions(@string, charDelimiter, 0, options, countExceedingBehaviour);
                                AssertOptions(@string, charDelimiter, 1, options, countExceedingBehaviour);
                                if(countDelimiters - 1 > 1) AssertOptions(@string, charDelimiter, countDelimiters - 1, options, countExceedingBehaviour);
                                if(countDelimiters > 1) AssertOptions(@string, charDelimiter, countDelimiters, options, countExceedingBehaviour);
                                AssertOptions(@string, charDelimiter, countDelimiters + 2, options, countExceedingBehaviour);
                                AssertOptions(@string, 'ა', 0, options, countExceedingBehaviour);
                                AssertOptions(@string, 'ა', 1, options, countExceedingBehaviour);
                                AssertOptions(@string, 'ა', 2, options, countExceedingBehaviour);
                            }
                        }
                    }
                }
            }
        }
    }
}
