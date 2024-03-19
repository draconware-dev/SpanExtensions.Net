using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/>, or a specified default value if the <see cref="Span{T}"/> contains no elements..
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="defaultValue">The default value to return if <paramref name="source"/> is empty.</param>
        /// <returns><paramref name="defaultValue"/> if <paramref name="source"/> is empty; otherwise, the last element in <paramref name="source"/>.</returns>
        public static T LastOrDefault<T>(this Span<T> source, T defaultValue)
        {
            return ReadOnlySpanExtensions.LastOrDefault(source, defaultValue);
        }

        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/> that satisfies a specified condition or a specified default value if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="defaultValue">The default value to return if <paramref name="source"/> is empty.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty or if no element passes the test specified by <paramref name="predicate"/>; otherwise, the last element in <paramref name="source"/> that passes the test specified by <paramref name="predicate"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        public static T LastOrDefault<T>(this Span<T> source, Predicate<T> predicate, T defaultValue)
        {
            return ReadOnlySpanExtensions.LastOrDefault(source, predicate, defaultValue);
        }
#if NETCOREAPP1_0_OR_GREATER
        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/>, or a specified default value if the <see cref="Span{T}"/> contains no elements..
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty; otherwise, the last element in <paramref name="source"/>.</returns>
        public static T? LastOrDefault<T>(this Span<T> source)
        {
            return ReadOnlySpanExtensions.LastOrDefault<T>(source);
        }

        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/> that satisfies a specified conditionor a default value if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty or if no element passes the test specified by <paramref name="predicate"/>; otherwise, the last element in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        public static T? LastOrDefault<T>(this Span<T> source, Predicate<T> predicate)
        {
            return ReadOnlySpanExtensions.LastOrDefault(source, predicate);
        }
#elif NETSTANDARD2_1
#pragma warning disable CS8603 // Possible null reference return.
        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/>, or a specified default value if the <see cref="Span{T}"/> contains no elements..
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty; otherwise, the last element in <paramref name="source"/>.</returns>
        public static T LastOrDefault<T>(this Span<T> source)
        {
            return ReadOnlySpanExtensions.LastOrDefault<T>(source);
        }

        /// <summary>
        /// Returns the last element in a <see cref="Span{T}"/> that satisfies a specified conditionor a default value if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(T) if <paramref name="source"/> is empty or if no element passes the test specified by <paramref name="predicate"/>; otherwise, the last element in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        public static T LastOrDefault<T>(this Span<T> source, Predicate<T> predicate)
        {
            return ReadOnlySpanExtensions.LastOrDefault(source, predicate);
        }
#pragma warning restore CS8603 // Possible null reference return.
#endif
    }
}