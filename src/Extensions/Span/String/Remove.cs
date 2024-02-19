using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns a new <see cref="Span{T}"/> in which all the characters in the current instance, beginning at <paramref name="startIndex"/> and continuing through the last position, have been deleted.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="startIndex">The zero-based position to begin deleting characters.</param>
        /// <returns>A new <see cref="Span{T}"/> that is equivalent to <paramref name="source"/> except for the removed characters.</returns>
        public static Span<T> Remove<T>(this Span<T> source, int startIndex)
        {
            return source[..startIndex];
        }
    }
}