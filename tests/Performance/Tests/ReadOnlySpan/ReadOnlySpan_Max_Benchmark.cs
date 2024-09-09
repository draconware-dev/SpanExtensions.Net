using BenchmarkDotNet.Attributes;

namespace SpanExtensions.Tests.Performance
{
    [MemoryDiagnoser(false)]
    public class ReadOnlySpan_Max_Benchmark
    {
        [Benchmark]
        [ArgumentsSource(nameof(GetArgs))]
        public int Max(int[] value)
        {
            return value.AsSpan().Max();
        }

        [Benchmark]
        [ArgumentsSource(nameof(GetArgs))]
        public int Max_Array(int[] value)
        {
            return value.Max();
        }

        [Benchmark]
        [ArgumentsSource(nameof(GetArgs))]
        public int Max_StraightForward(int[] value)
        {
            ReadOnlySpan<int> source = value;
            int max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                int current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        public IEnumerable<int[]> GetArgs()
        {
            Random random = new Random(3);

            int[] choices = Enumerable.Range(0, 10000).ToArray();

            int[] data = random.GetItems(choices, 10);

            yield return data;

            data = random.GetItems(choices, 100);

            yield return data;

            data = random.GetItems(choices, 1000);

            yield return data;
        }
    }
}