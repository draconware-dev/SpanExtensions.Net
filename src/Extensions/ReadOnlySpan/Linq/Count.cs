using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
#if !NET8_0_OR_GREATER // support for this method has been added in .Net 8. Just include it for backward-compatibility. 

        /// <summary>
        /// Returns the number of elements in a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> whose elements are to be counted.</param>
        /// <returns>The number of elements in <paramref name="source"/>.</returns>
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this ReadOnlySpan<T> source)
        {
            return source.Length;
        }
#endif
        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> that contains elements to be tested and counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A number that represents how many elements in <paramref name="source"/> satisfy the condition in the predicate function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception> 
        /// <exception cref="OverflowException">The number of elements in <paramref name="source"/> is larger than <see cref="int.MaxValue"/>.</exception>
        public static int Count<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            if(predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            int count = 0;

            foreach(var item in source)
            {
                checked
                {
                    if(predicate(item))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}