using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the first element in a <see cref="ReadOnlySpan{T}"/>, or a specified default value if the <see cref="ReadOnlySpan{T}"/> contains no elements..
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the first element of.</param>
        /// <param name="defaultValue">The default value to return if <paramref name="source"/> is empty.</param>
        /// <returns><paramref name="defaultValue"/> if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.</returns>
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, T defaultValue)
        {
            if(source.IsEmpty)
            {
                return defaultValue;
            }
            return source[0];
        }

        /// <summary>
        /// Returns the first element in a <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition or a specified default value if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">The default value to return if <paramref name="source"/> is empty.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty or if no element passes the test specified by <paramref name="predicate"/>; otherwise, the first element in <paramref name="source"/> that passes the test specified by <paramref name="predicate"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, Predicate<T> predicate, T defaultValue)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if(source.IsEmpty)
            {
                return defaultValue;
            }
            for(int i = 0; i < source.Length; i++)
            {
                T item = source[i];

                if(predicate(item))
                {
                    return item;
                }
            }
            return defaultValue;
        }
#if NETCOREAPP1_0_OR_GREATER
        /// <summary>
        /// Returns the first element in a <see cref="ReadOnlySpan{T}"/>, or a specified default value if the <see cref="ReadOnlySpan{T}"/> contains no elements..
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the first element of.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.</returns>
        public static T? FirstOrDefault<T>(this ReadOnlySpan<T> source)
        {
            if(source.IsEmpty)
            {
                return default(T);
            }
            return source[0];
        }

        /// <summary>
        /// Returns the first element in a <see cref="ReadOnlySpan{T}"/> that satisfies a specified conditionor a default value if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty or if no element passes the test specified by <paramref name="predicate"/>; otherwise, the first element in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        public static T? FirstOrDefault<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if(source.IsEmpty)
            {
                return default(T);
            }
            for(int i = 0; i < source.Length; i++)
            {
                T item = source[i];

                if(predicate(item))
                {
                    return item;
                }
            }
            return default(T); 
        }
#elif NETSTANDARD2_1
#pragma warning disable CS8603 // Possible null reference return.
        /// <summary>
        /// Returns the first element in a <see cref="ReadOnlySpan{T}"/>, or a specified default value if the <see cref="ReadOnlySpan{T}"/> contains no elements..
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the first element of.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty; otherwise, the first element in <paramref name="source"/>.</returns>
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source)
        {
            if(source.IsEmpty)
            {
                return default(T);
            }
            return source[0];
        }

        /// <summary>
        /// Returns the first element in a <see cref="ReadOnlySpan{T}"/> that satisfies a specified conditionor a default value if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty or if no element passes the test specified by <paramref name="predicate"/>; otherwise, the first element in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        public static T FirstOrDefault<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if(source.IsEmpty)
            {
                return default(T);

            }
            for(int i = 0; i < source.Length; i++)
            {
                T item = source[i];

                if(predicate(item))
                {
                    return item;
                }
            }
            return default(T);
        }
#pragma warning restore CS8603 // Possible null reference return.
#endif
    }
}