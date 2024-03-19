using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Determines whether any element in <paramref name="source"/> satisfies a condition. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="predicate">The Condition to be satisfied.</param>
        /// <returns>A <see cref="bool"/> indicating whether or not any elements satisified the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool Any<T>(this Span<T> source, Predicate<T> predicate) where T : IEquatable<T>
        {
            return ReadOnlySpanExtensions.Any(source, predicate);
        }
    }
}