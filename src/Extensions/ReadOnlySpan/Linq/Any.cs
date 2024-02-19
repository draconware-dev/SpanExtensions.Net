using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Determines whether any element in <paramref name="source"/> satisfies a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="predicate">The condition to be satisfied.</param>
        /// <returns>A <see cref="bool"/> indicating whether or not any elements satisified the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool Any<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            for(int i = 0; i < source.Length; i++)
            {
                if(predicate(source[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}