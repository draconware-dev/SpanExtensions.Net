using SpanExtensions.Enumerators;

namespace SpanExtensions.Tests.Fuzzing
{
    /// <summary>
    /// Extension methods to convert <see langword="ref struct"/> enumerators into <see cref="IEnumerable{T}"/>.
    /// This obviously defeats the purpose of using spans. <strong>This is for testing only</strong>.
    /// </summary>
    public static class ToSystemEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitEnumerator<T> spanEnumerator, int maxCount = 100) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitWithCountEnumerator<T> spanEnumerator, int maxCount = 100) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitStringSplitOptionsEnumerator spanEnumerator, int maxCount = 100)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitStringSplitOptionsWithCountEnumerator spanEnumerator, int maxCount = 100)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitAnyEnumerator<T> spanEnumerator, int maxCount = 100) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitAnyWithCountEnumerator<T> spanEnumerator, int maxCount = 100) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitAnyStringSplitOptionsEnumerator spanEnumerator, int maxCount = 100)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitAnyStringSplitOptionsWithCountEnumerator spanEnumerator, int maxCount = 100)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitSequenceEnumerator<T> spanEnumerator, int maxCount = 100) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<T>> ToSystemEnumerable<T>(this SpanSplitSequenceWithCountEnumerator<T> spanEnumerator, int maxCount = 100) where T : IEquatable<T>
        {
            List<T[]> list = [];

            foreach(ReadOnlySpan<T> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitSequenceStringSplitOptionsEnumerator spanEnumerator, int maxCount = 100)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }

        public static IEnumerable<IEnumerable<char>> ToSystemEnumerable(this SpanSplitSequenceStringSplitOptionsWithCountEnumerator spanEnumerator, int maxCount = 100)
        {
            List<char[]> list = [];

            foreach(ReadOnlySpan<char> element in spanEnumerator)
            {
                list.Add(element.ToArray());

                if(list.Count >= maxCount)
                {
                    throw new IndexOutOfRangeException($"Enumeration exceeded {maxCount}.");
                }
            }

            return list;
        }
    }
}
