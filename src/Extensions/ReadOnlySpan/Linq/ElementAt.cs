﻿using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param> 
        /// <returns>The element at the specified position in <paramref name="source"/>. </returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than or equal to the number of elements in <paramref name="source"/>.</exception> 
        /// <remarks>This method throws an exception if <paramref name="index"/> is out of range. To instead return a default value when the specified index is out of range, use the <see cref="ElementAtOrDefault{T}(ReadOnlySpan{T}, int)"/> method. </remarks> 
        public static T ElementAt<T>(this ReadOnlySpan<T> source, int index)
        {
            ExceptionHelpers.ThrowIfOutOfArrayBounds(index, source.Length, nameof(index));
            return source[index];
        }

        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the last element of.</param>
        /// <param name="index">The index of the element to retrieve, which is either from the beginning or the end of the sequence.</param>
        /// <returns>The element at the specified position in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the bounds of <paramref name="source"/>.</exception> 
        /// <remarks>This method throws an exception if <paramref name="index"/> is out of range. To instead return a default value when the specified index is out of range, use the <see cref="ElementAtOrDefault{T}(ReadOnlySpan{T}, Index)"/> method. </remarks> 
        public static T ElementAt<T>(this ReadOnlySpan<T> source, Index index)
        {
            ExceptionHelpers.ThrowIfOutOfArrayBounds(index.Value, source.Length, nameof(index));
            return source[index];
        }
    }
}