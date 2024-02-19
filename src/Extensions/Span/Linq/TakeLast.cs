using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns a new <see cref="Span{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to take from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="Span{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="Span{T}.Empty"/>.</remarks>
        public static Span<T> TakeLast<T>(this Span<T> source, int count)
        {
            if(count < 0)
            {
                return Span<T>.Empty;
            }
            return source[(source.Length - count)..];
        }
    }
}