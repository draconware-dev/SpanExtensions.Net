using System;
using System.Collections.Generic;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the input typed as <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">The sequence to type as <see cref="IEnumerable{T}"/>.</param>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <returns>The input sequence typed as <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this ReadOnlySpan<T> source)
        {
            ReadOnlySpan<T>.Enumerator e = source.GetEnumerator();

            while(e.MoveNext())
            {
                yield return e.Current;
            }
        }
    }
}