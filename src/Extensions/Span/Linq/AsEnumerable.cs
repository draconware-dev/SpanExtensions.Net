using System;
using System.Collections.Generic;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns the input typed as <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The sequence to type as <see cref="IEnumerable{T}"/>.</param>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <returns>The input sequence typed as <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this Span<T> source)
        {
            return ReadOnlySpanExtensions.AsEnumerable<T>(source);
        }
    }
}