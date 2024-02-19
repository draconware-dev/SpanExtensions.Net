using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Bypasses a specified number of elements in <paramref name="source"/> and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The Number of elements to bypass.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains the elements that occur after <paramref name="count"/> elements have been bypassed in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ReadOnlySpan<T> Skip<T>(this ReadOnlySpan<T> source, int count)
        {
            return source[count..];
        }
    }
}