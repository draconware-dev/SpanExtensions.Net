using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        public static class SplitAnyString
        {
            public static TheoryData<int, int, int, float> SplitAnyData(int iterations)
            {
                TheoryData<int, int, int, float> data = new();

                foreach(int length in new MultiplierRange(1, 1000, 10).And([0]))
                {
                    foreach(int delimitersLength in ((IEnumerable<int>)[0, 1, 5, 25, 50]).Where(x => x <= length * 3))
                    {
                        foreach(float delimitersOccurencePart in GetParts(delimitersLength))
                        {
                            data.Add(iterations, length, delimitersLength, delimitersOccurencePart);
                        }
                    }
                }

                return data;

                static IEnumerable<float> GetParts(int delimitersLength)
                {
                    return delimitersLength switch
                    {
                        0 => [0f],
                        1 => [0f, 1f],
                        _ => [0f, 0.5f, 1f]
                    };
                }
            }

            public sealed class SplitAnyWithoutParameters
            {
                public static readonly TheoryData<int, int, int, float> _SplitAnyData = SplitAnyData(15000);

                [Theory]
                [MemberData(nameof(_SplitAnyData))]
                public void Fuzz(int iterations, int length, int delimitersLength, float delimitersOccurencePart)
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

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        string @string = GenerateRandomString(length);
                        char[] charDelimiters = Enumerable.Range(0, delimitersLength).Select(i =>
                            i < delimitersLength * delimitersOccurencePart ? @string.RandomElementOrDefault()
                            : (char)('ა' + i)
                        ).ToArray();
                        foreach(StringSplitOptions options in stringSplitOptions)
                        {
                            AssertOptions(@string, charDelimiters, options);
                        }
                    }
                }
            }

            public sealed class SplitAnyWithCount
            {
                public static readonly TheoryData<int, int, int, float> _SplitAnyData = SplitAnyData(2000);

                [Theory]
                [MemberData(nameof(_SplitAnyData))]
                public void Fuzz(int iterations, int length, int delimitersLength, float delimitersOccurencePart)
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

                    for(int iteration = 0; iteration < iterations; iteration++)
                    {
                        string @string = GenerateRandomString(length);
                        char[] charDelimiters = Enumerable.Range(0, delimitersLength).Select(i =>
                            i < delimitersLength * delimitersOccurencePart ? @string.RandomElementOrDefault()
                            : (char)('ა' + i)
                        ).ToArray();
                        int countDelimiters = @string.AsSpan().Count(charDelimiters);
                        foreach(StringSplitOptions options in stringSplitOptions)
                        {
                            foreach(CountExceedingBehaviour countExceedingBehaviour in countExceedingBehaviours)
                            {
                                AssertOptions(@string, charDelimiters, 0, options, countExceedingBehaviour);
                                AssertOptions(@string, charDelimiters, 1, options, countExceedingBehaviour);
                                if(countDelimiters - 1 > 1) AssertOptions(@string, charDelimiters, countDelimiters - 1, options, countExceedingBehaviour);
                                if(countDelimiters > 1) AssertOptions(@string, charDelimiters, countDelimiters, options, countExceedingBehaviour);
                                AssertOptions(@string, charDelimiters, countDelimiters + 2, options, countExceedingBehaviour);
                            }
                        }
                    }
                }
            }
        }
    }
}
