using System;
using System.Collections.Generic;
using System.Numerics;

namespace SpanExtensions
{
    /// <summary>
    /// Extension Methods for <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns the maximum value in a generic sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
        /// <param name="source">A <see cref="Span{TSource}"/> to determine the maximum value of.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>The value with the maximum key in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><typeparamref name="TSource"/> is a primitive type and <paramref name="source"/> is empty.</exception>
        /// <remarks>If <paramref name="source"/> is empty and <typeparamref name="TSource"/> is a non-nullable struct, such as a primitive type, an <see cref="InvalidOperationException"/> is thrown.</remarks>
        public static TSource MaxBy<TSource, TKey>(this Span<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable<TKey>
        {
            return ReadOnlySpanExtensions.MaxBy(source, keySelector);
        }

        /// <summary>
        /// Returns the maximum value in a generic sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
        /// <param name="source">A <see cref="Span{TSource}"/> to determine the maximum value of.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="comparer">The <see cref="IComparer{TSource}"/> to compare keys.</param>
        /// <returns>The value with the maximum key in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><typeparamref name="TSource"/> is a primitive type and <paramref name="source"/> is empty.</exception>
        /// <remarks>If <paramref name="source"/> is empty and <typeparamref name="TSource"/> is a non-nullable struct, such as a primitive type, an <see cref="InvalidOperationException"/> is thrown.</remarks>
        public static TSource MaxBy<TSource, TKey>(this Span<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return ReadOnlySpanExtensions.MaxBy(source, keySelector, comparer);
        }
    }
}