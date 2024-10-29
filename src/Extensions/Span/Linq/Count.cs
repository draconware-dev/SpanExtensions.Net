using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Returns the number of elements in a <see cref="Span{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> whose elements are to be counted.</param>
        /// <returns>The number of elements in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this Span<T> source)
        {
            return ReadOnlySpanExtensions.Count<T>(source);
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> that contains elements to be tested and counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A number that represents how many elements in <paramref name="source"/> satisfy the condition in the predicate function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this Span<T> source, Predicate<T> predicate)
        {
            return ReadOnlySpanExtensions.Count(source, predicate);
        }

#if !NET8_0_OR_GREATER
        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="source"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> whose elements are to be counted.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of elements eqaul to <paramref name="value"/> in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this Span<T> source, T value) where T : IEquatable<T>
        {
            return ReadOnlySpanExtensions.Count(source, value);
        }

        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="source"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> whose elements are to be counted.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of elements eqaul to <paramref name="value"/> in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this Span<T> source, ReadOnlySpan<T> value) where T : IEquatable<T>
        {
            return ReadOnlySpanExtensions.Count(source, value);
        }
#endif
    }
}