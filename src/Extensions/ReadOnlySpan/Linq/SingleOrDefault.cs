using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Returns the only element in <see cref="ReadOnlySpan{T}"/>, and throws an exception if there is not exactly one element in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the single element of.</param>
        /// <returns>The single element in <paramref name="source"/>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> contains more than one element -or- <paramref name="source"/> is empty.</exception>
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, T defaultValue)
        {
            if(source.Length != 1)
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
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, Predicate<T> predicate, T defaultValue)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            T single = defaultValue!;
            bool hassingle = false;
            for(int i = 0; i < source.Length; i++)
            {
                T item = source[i];

                if(predicate(item))
                {
                    if(hassingle)
                    {
                        throw new InvalidOperationException($"{nameof(source)} must contain only one element that matches {nameof(predicate)}.");
                    }
                    single = item;
                    hassingle |= true;
                }
            }
            return single;
        }
#if NETCOREAPP1_0_OR_GREATER
        /// <summary>
        /// Returns the only element in <see cref="ReadOnlySpan{T}"/>, and throws an exception if there is not exactly one element in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the single element of.</param>
        /// <returns>The single element in <paramref name="source"/>.</returns>
        public static T? SingleOrDefault<T>(this ReadOnlySpan<T> source)
        {
            if(source.Length != 1)
            {
                return default(T);
            }
            return source[0];
        }

        /// <summary>
        /// Returns the only element in <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element in <paramref name="source"/> that satisfies a condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>. -or- More than one element satisfies the condition in <paramref name="predicate"/>. -or- <paramref name="source"/> is empty.</exception>
        public static T? SingleOrDefault<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            T? single = default;
            bool hassingle = false;
            for(int i = 0; i < source.Length; i++)
            {
                T item = source[i];

                if(predicate(item))
                {
                    if(hassingle)
                    {
                        throw new InvalidOperationException($"{nameof(source)} must contain only one element that matches {nameof(predicate)}.");
                    }
                    single = item;
                    hassingle |= true;
                }
            }
            return single;
        }
#elif NETSTANDARD2_1
#pragma warning disable CS8603 // Possible null reference return.
        /// <summary>
        /// Returns the only element in <see cref="ReadOnlySpan{T}"/>, and throws an exception if there is not exactly one element in the <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the single element of.</param>
        /// <returns>The single element in <paramref name="source"/>.</returns>
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source)
        {
            if(source.Length != 1)
            {
                return default(T);
            }
            return source[0];
        }

        /// <summary>
        /// Returns the only element in <see cref="ReadOnlySpan{T}"/> that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element in <paramref name="source"/> that satisfies a condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>. -or- More than one element satisfies the condition in <paramref name="predicate"/>. -or- <paramref name="source"/> is empty.</exception>
        public static T SingleOrDefault<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            T single = default;
            bool hassingle = false;
            for(int i = 0; i < source.Length; i++)
            {
                T item = source[i];

                if(predicate(item))
                {
                    if(hassingle)
                    {
                        throw new InvalidOperationException($"{nameof(source)} must contain only one element that matches {nameof(predicate)}.");
                    }
                    single = item;
                    hassingle |= true;
                }
            }
            return single;
        }
#pragma warning restore CS8603 // Possible null reference return.
#endif
    }
}