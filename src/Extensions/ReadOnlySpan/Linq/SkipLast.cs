using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> that contains the elements from source with the last <paramref name="count"/> elements of the source collection omitted.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to omit from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the elements from <paramref name="source"/> minus <paramref name="count"/> elements from the end of the collection.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ReadOnlySpan<T> SkipLast<T>(this ReadOnlySpan<T> source, int count)
        {
            if(count < 0)
            {
                return ReadOnlySpan<T>.Empty;
            }
            return source[..(source.Length - count)];
        }
    }
}