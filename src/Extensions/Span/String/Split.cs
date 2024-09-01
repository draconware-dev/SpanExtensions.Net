using System;
using System.Collections;
using SpanExtensions.Enumerators;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {

        /// <summary>
        /// Splits a <see cref="Span{T}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to be split.</param>
        /// <param name="delimiter">An instance of <typeparamref name="T"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitEnumerator<T> Split<T>(this Span<T> source, T delimiter) where T : IEquatable<T>
        {
            return new SpanSplitEnumerator<T>(source, delimiter);
        }

        /// <summary>
        /// Splits a <see cref="Span{T}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to be split.</param>
        /// <param name="delimiter">An instance of <typeparamref name="T"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitWithCountEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitWithCountEnumerator<T> Split<T>(this Span<T> source, T delimiter, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements) where T : IEquatable<T>
        {
            return new SpanSplitWithCountEnumerator<T>(source, delimiter, count, countExceedingBehaviour);
        }

        /// <summary>
        /// Splits a <see cref="Span{Char}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/> and the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Char}"/> to be split.</param>
        /// <param name="delimiter">A <see cref="char"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitStringSplitOptionsEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitStringSplitOptionsEnumerator Split(this Span<char> source, char delimiter, StringSplitOptions options)
        {
            return new SpanSplitStringSplitOptionsEnumerator(source, delimiter, options);
        }

        /// <summary>
        /// Splits a <see cref="Span{Char}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/> and the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Char}"/> to be split.</param>
        /// <param name="delimiter">A <see cref="char"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitAnyStringSplitOptionsWithCountEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitStringSplitOptionsWithCountEnumerator Split(this Span<char> source, char delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements)
        {
            return new SpanSplitStringSplitOptionsWithCountEnumerator(source, delimiter, count, options, countExceedingBehaviour);
        }
    }
}