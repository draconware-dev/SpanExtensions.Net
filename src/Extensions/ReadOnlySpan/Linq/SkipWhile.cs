using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Bypasses elements in <paramref name="source"/> as long as a <paramref name="condition"/> is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="condition">A function to test each element for a condition.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains the elements from <paramref name="source"/> starting at the first element in the linear series that does not pass the specified <paramref name="condition" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="condition"/> is null.</exception>
        public static ReadOnlySpan<T> SkipWhile<T>(this ReadOnlySpan<T> source, Predicate<T> condition)
        {
            int count = 0;
            while(count < source.Length)
            {
                T t = source[count];
                if(!condition(t))
                {
                    return source.Skip(count);
                }
                count++;
            }
            return ReadOnlySpan<T>.Empty;
        }
    }
}