
using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {

        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> in which all the characters in the current instance, beginning at <paramref name="startIndex"/> and continuing through the last position, have been deleted.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="startIndex">The zero-based position to begin deleting characters.</param>
        /// <returns>A new  <see cref="ReadOnlySpan{T}"/> that is equivalent to this <see cref="ReadOnlySpan{T}"/> except for thse removed characters.</returns>  
        public static ReadOnlySpan<T> Remove<T>(this ReadOnlySpan<T> source, int startIndex)
        {
            return source[..startIndex];
        }
    }
}