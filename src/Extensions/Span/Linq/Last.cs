using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <returns>The last element in <paramref name="source"/>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty.</exception>
        public static T Last<T>(this Span<T> source)
        {
            return ReadOnlySpanExtensions.Last<T>(source);
        }

        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The last element in <paramref name="source"/> that passes the test in the specified <paramref name="predicate"/> function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>. -or- <paramref name="source"/> is empty.</exception>
        public static T Last<T>(this Span<T> source, Predicate<T> predicate)
        {
            return ReadOnlySpanExtensions.Last<T>(source, predicate);
        }
    }
}