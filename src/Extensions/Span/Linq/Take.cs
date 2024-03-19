using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="count">The Number of elements to take.</param>
        /// <returns>A <see cref="Span{T}"/> that contains <paramref name="count"/> elements from the start of the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Span<T> Take<T>(this Span<T> source, int count)
        {
            return source[..count];
        }
    }
}