using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        ///  <summary>
        /// Returns elements from <paramref name="source"/> as long as a specified <paramref name="condition"/> is true, and then skips the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="condition">A function to test each element for a condition.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains elements from <paramref name="source"/> that occur before the element at which the <paramref name="condition"/> no longer passes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="condition"/> is null.</exception>
        public static ReadOnlySpan<T> TakeWhile<T>(this ReadOnlySpan<T> source, Predicate<T> condition)
        {
            int count = 0;
            while(count < source.Length)
            {
                T t = source[count];
                if(!condition(t))
                {
                    return source.Take(count);
                }
                count++;
            }
            return ReadOnlySpan<T>.Empty;
        }
    }
}