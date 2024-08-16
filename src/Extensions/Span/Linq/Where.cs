using System;
using System.Collections.Generic;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A <see cref="Span{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public static IEnumerable<T> Where<T>(this Span<T> source, Predicate<T> predicate)
        {
            return ReadOnlySpanExtensions.Where(source, predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A <see cref="Span{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public static IEnumerable<T> Where<T>(this Span<T> source, Func<T, int, bool> predicate)
        {
            return ReadOnlySpanExtensions.Where(source, predicate);
        }
    }
}