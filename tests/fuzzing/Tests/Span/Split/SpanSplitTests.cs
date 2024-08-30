using static SpanExtensions.Tests.Fuzzing.TestHelper;

namespace SpanExtensions.Tests.Fuzzing
{
    public static partial class SpanSplitTests
    {
        static readonly IEnumerable<StringSplitOptions> stringSplitOptions = GetAllStringSplitOptions();
        static readonly CountExceedingBehaviour[] countExceedingBehaviours = (CountExceedingBehaviour[])Enum.GetValues(typeof(CountExceedingBehaviour));
    }
}
