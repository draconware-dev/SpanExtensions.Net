using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns the only element in <see cref="Span{T}"/>, and throws an exception if there is not exactly one element in the <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the single element of.</param>
        /// <returns>The single element in <paramref name="source"/>.</returns>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> contains more than one element -or- <paramref name="source"/> is empty.</exception>
        public static T Single<T>(this Span<T> source)
        {
            return ReadOnlySpanExtensions.Single<T>(source);
        }

        /// <summary>
        /// Returns the only element in <see cref="Span{T}"/> that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element in <paramref name="source"/> that satisfies a condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="InvalidOperationException">No element satisfies the condition in <paramref name="predicate"/>. -or- More than one element satisfies the condition in <paramref name="predicate"/>. -or- <paramref name="source"/> is empty.</exception>
        public static T Single<T>(this Span<T> source, Predicate<T> predicate)
        {
            return ReadOnlySpanExtensions.Single<T>(source, predicate);
        }
    }
}