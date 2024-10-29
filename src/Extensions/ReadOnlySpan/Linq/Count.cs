using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the number of elements in a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> whose elements are to be counted.</param>
        /// <returns>The number of elements in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this ReadOnlySpan<T> source)
        {
            return source.Length;
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> that contains elements to be tested and counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A number that represents how many elements in <paramref name="source"/> satisfy the condition in the predicate function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            int count = 0;

            foreach(var item in source)
            {
                checked
                {
                    if(predicate(item))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

#if !NET8_0_OR_GREATER
        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="source"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> whose elements are to be counted.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of elements eqaul to <paramref name="value"/> in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this ReadOnlySpan<T> source, T value) where T : IEquatable<T>
        {
            int count = 0;

            foreach(var item in source)
            {
                if(item.Equals(value))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="source"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> whose elements are to be counted.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of elements eqaul to <paramref name="value"/> in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> value) where T : IEquatable<T>
        {
            int count = 0;
            int current = 0;

            while((current = source.IndexOf(value)) != -1)
            {
                source = source.Slice(current + value.Length);
                count++;
            }

            return count;
        }
#endif
    }
}