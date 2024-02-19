using System;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Determines whether all elements in <paramref name="source"/> satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="predicate">The condition to be satisfied.</param>
        /// <returns>A <see cref="bool"/> indicating whether or not every element in <paramref name="source"/> satisified the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool All<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            for(int i = 0; i < source.Length; i++)
            {
                if(!predicate(source[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}