namespace SpanExtensions.Tests.UnitTests
{
    public static partial class ReadOnlySpanSplitTests
    {
        internal static readonly IEnumerable<StringSplitOptions> stringSplitOptions = TestHelper.GetAllStringSplitOptions();
        internal static readonly CountExceedingBehaviour[] countExceedingBehaviours = Enum.GetValues<CountExceedingBehaviour>();
        internal static readonly char[][] EmptyNestedCharArray = [[]];
        internal static readonly char[][] NestedABBAArray = [['a', 'b', 'b', 'a']];
        internal static readonly char[] ABBAArray = ['a', 'b', 'b', 'a'];
        internal static readonly CountExceedingBehaviour InvalidCountExceedingBehaviour = (CountExceedingBehaviour)byte.MaxValue;
        internal static readonly StringSplitOptions InvalidStringSplitOptions = (StringSplitOptions)byte.MaxValue;
    }
}
