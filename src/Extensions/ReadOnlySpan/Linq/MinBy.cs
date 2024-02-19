﻿using System;
using System.Collections.Generic;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the minimum value in a generic sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{TSource}"/> to determine the minimum value of.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>The value with the minimum key in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><typeparamref name="TSource"/> is a primitive type and <paramref name="source"/> is empty.</exception>
        /// <remarks>If <paramref name="source"/> is empty and <typeparamref name="TSource"/> is a non-nullable struct, such as a primitive type, an <see cref="InvalidOperationException"/> is thrown.</remarks>
        public static TSource MinBy<TSource, TKey>(this ReadOnlySpan<TSource> source, Func<TSource, TKey> keySelector) where TKey : IComparable<TKey>
        {
            if(keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if(source.IsEmpty)
            {
                throw new InvalidOperationException($"{nameof(source)} is empty");
            }

            TSource min = source[0];
            TKey minKey = keySelector(min);
            for(int i = 1; i < source.Length; i++)
            {
                TSource value = source[i];
                TKey current = keySelector(value);
                if(current.CompareTo(minKey) < 0)
                {
                    min = value;
                    minKey = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in a generic sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{TSource}"/> to determine the minimum value of.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="comparer">The <see cref="IComparer{TSource}"/> to compare keys.</param>
        /// <returns>The value with the minimum key in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><typeparamref name="TSource"/> is a primitive type and <paramref name="source"/> is empty.</exception>
        /// <remarks>If <paramref name="source"/> is empty and <typeparamref name="TSource"/> is a non-nullable struct, such as a primitive type, an <see cref="InvalidOperationException"/> is thrown.</remarks>
        public static TSource MinBy<TSource, TKey>(this ReadOnlySpan<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if(keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if(comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }
            if(source.IsEmpty)
            {
                throw new InvalidOperationException($"{nameof(source)} is empty");
            }

            TSource min = source[0];
            TKey minKey = keySelector(min);
            for(int i = 1; i < source.Length; i++)
            {
                TSource value = source[i];
                TKey current = keySelector(value);
                if(comparer.Compare(current, minKey) < 0)
                {
                    min = value;
                    minKey = current;
                }
            }
            return min;
        }
    }
}