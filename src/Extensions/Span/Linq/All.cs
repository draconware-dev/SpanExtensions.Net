using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Determines whether all elements in <paramref name="source"/> satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="predicate">The Condition to be satisfied.</param>
        /// <returns>A <see cref="bool"/> indicating whether or not every element in <paramref name="source"/> satisified the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool All<T>(this Span<T> source, Predicate<T> predicate) where T : IEquatable<T>
        {
            return ReadOnlySpanExtensions.All(source, predicate);
        }
    }
}