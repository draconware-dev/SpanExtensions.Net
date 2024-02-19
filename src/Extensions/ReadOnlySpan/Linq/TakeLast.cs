using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to take from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ReadOnlySpan<T> TakeLast<T>(this ReadOnlySpan<T> source, int count)
        {
            if(count < 0)
            {
                return ReadOnlySpan<T>.Empty;
            }
            return source[(source.Length - count)..];
        }
    }
}