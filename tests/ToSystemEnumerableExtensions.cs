using SpanExtensions.Enumerators;

namespace Tests
{
    /// <summary>
    /// Extension methods to convert <see langword="ref struct"/> enumerators into <see cref="IEnumerable{T}"/>.
    /// This obviously defeats the purpose of using spans. <strong>This is for testing only</strong>.
    /// </summary>
    public static class ToSystemEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitEnumerator<T> spanEnumerator) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());
            }

            return list;
        }

        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitWithCountEnumerator<T> spanEnumerator) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitStringSplitOptionsEnumerator spanEnumerator)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitStringSplitOptionsWithCountEnumerator spanEnumerator)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());
            }

            return list;
        }
    }
}
