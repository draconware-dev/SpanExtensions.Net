namespace SpanExtensions.Tests.UnitTests
{
    public class LinqMinMaxTests
    {
        [Theory]
        [MemberData(nameof(GetMaxData))]
        public void Max(int[] source, int max)
        {
            ReadOnlySpan<int> span = source.AsSpan();
            Assert.Equal(max, span.Max());
        }

        [Theory]
        [MemberData(nameof(GetMinData))]
        public void Min(int[] source, int min)
        {
            ReadOnlySpan<int> span = source.AsSpan();
            Assert.Equal(min, span.Min());
        }

        public static TheoryData<int[], int> GetMaxData()
        {
            var data = new TheoryData<int[], int>();

            var Samples10 = GetSampleSetInts(10);
            var Samples100 = GetSampleSetInts(100);
            var Samples1000 = GetSampleSetInts(1000);

            int max10 = Samples10.Max();
            int max100 = Samples100.Max();
            int max1000 = Samples1000.Max();

            data.Add(Samples10, max10);
            data.Add(Samples100, max100);
            data.Add(Samples1000, max1000);

            return data;
        }

        public static TheoryData<int[], int> GetMinData()
        {
            var data = new TheoryData<int[], int>();

            var Samples10 = GetSampleSetInts(10);
            var Samples100 = GetSampleSetInts(100);
            var Samples1000 = GetSampleSetInts(1000);

            int min10 = Samples10.Min();
            int min100 = Samples100.Min();
            int min1000 = Samples1000.Min();

            data.Add(Samples10, min10);
            data.Add(Samples100, min100);
            data.Add(Samples1000, min1000);

            return data;
        }

        static int[] GetSampleSetInts(int count)
        {
            Random random = new Random(count);

            int[] sample = new int[count];

            for(int i = 0; i < count; i++)
            {
                sample[i] = random.Next();
            }

            return sample;
        }
    }
}
