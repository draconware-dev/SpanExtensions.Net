using System;
using System.Collections.Generic;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public static IEnumerable<T> Where<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            ReadOnlySpan<T>.Enumerator e = source.GetEnumerator();

            while(e.MoveNext())
            {
                T current = e.Current;

                if(predicate(current)) 
                {
                    yield return current;
                }
            }
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public static IEnumerable<T> Where<T>(this ReadOnlySpan<T> source, Func<T, int, bool> predicate)
        {
            for(int i = 0; i < source.Length; i++)
            {
                T current = source[i];

                if(predicate(current, i))
                {
                    yield return current;
                }
            }
        }
    }
}