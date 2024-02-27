using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the last element in a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the last element of.</param>
        /// <returns>The last element in <paramref name="source"/>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty.</exception>
        public static T Last<T>(this ReadOnlySpan<T> source)
        {
            if(source.IsEmpty)
            {
                throw new InvalidOperationException($"{nameof(source)} cannot be empty.");
            }
            return source[^1];
        }

        /// <summary>
        /// Returns the last element in a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The last element in <paramref name="source"/> that passes the test in the specified <paramref name="predicate"/> function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>. -or- <paramref name="source"/> is empty.</exception>
        public static T Last<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if(source.IsEmpty)
            {
                throw new InvalidOperationException($"{nameof(source)} cannot be empty.");
            }
            for(int i = source.Length - 1; i > 0; i--)
            {
                T item = source[i];

                if(predicate(item))
                {
                    return item;
                }
            }
            throw new InvalidOperationException("No element matched the specified condition.");
        }
    }
}